using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using XMLDatabase.CustomExceptions;
using XMLDatabase.Interfaces;
using XMLDatabase.Models;

namespace XMLDatabase.DataAccessors
{
    public class UserDataAccessor : IUserDataAccessor
    {
        private string fileName;
        public UserDataAccessor()
        {
#if DEBUG
            fileName = @"E:\University\AnIII\Sem II\PS\Lab\Assigment2\Bookstore\XMLDatabase\bin\Debug\Users.xml";
#else
            fileName = @"E:\University\AnIII\Sem II\PS\Lab\Assigment2\Bookstore\XMLDatabase\bin\Release\Users.xml";
#endif
        }

        public bool Insert(Models.User user)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    XmlTextWriter textWrite = new XmlTextWriter(fileName, null);
                    textWrite.WriteStartDocument();
                    textWrite.WriteStartElement("User");
                    textWrite.WriteEndElement();
                    textWrite.Close();
                }

                // Create the XML docment by loading the file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                // Creating User node
                XmlElement subNode = xmlDoc.CreateElement("User");

                // Getting the maximum Id based on the XML data already stored
                string strId = CommonMethods.GetMaxValue(xmlDoc, "Users" + "/" + "User" + "/" + "Id").ToString();

                // Adding Id column. Auto generated column
                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Id", strId));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Username", user.Username));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Password", user.Password));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Firstname", user.Firstname));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Lastname", user.Lastname));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Mobile", user.Mobile));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Mail", user.Mail));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Role", user.Role));
                xmlDoc.DocumentElement.AppendChild(subNode);

                // Saving the file after adding the new employee node
                xmlDoc.Save(fileName);

                return true;
            }
            catch(Exception ex)
            {
                throw new DatabaseException("User cannot be inserted!", ex.InnerException);
            }
        }

        public bool Update(Models.User user)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlDocument objXmlDocument = new XmlDocument();
                    objXmlDocument.Load(fileName);

                    XmlNode node = objXmlDocument.SelectSingleNode("//User[Id='" + user.Id + "']");

                    if (node != null)
                    {
                        // Assigining all the values
                        node.SelectNodes("Id").Item(0).FirstChild.Value = user.Id.ToString();
                        node.SelectNodes("Username").Item(0).FirstChild.Value = user.Username;
                        node.SelectNodes("Password").Item(0).FirstChild.Value = user.Password;
                        node.SelectNodes("Firstname").Item(0).FirstChild.Value = user.Firstname;
                        node.SelectNodes("Lastname").Item(0).FirstChild.Value = user.Lastname;
                        node.SelectNodes("Mobile").Item(0).FirstChild.Value = user.Mobile;
                        node.SelectNodes("Mail").Item(0).FirstChild.Value = user.Mail;
                        node.SelectNodes("Role").Item(0).FirstChild.Value = user.Role;
                    }

                    objXmlDocument.Save(fileName);

                    return true;
                }
                else
                {
                    Exception ex = new Exception("Database file does not exist in the folder");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("User cannot be updated!", ex.InnerException);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlDocument objXmlDocument = new XmlDocument();
                    objXmlDocument.Load(fileName);

                    XmlNode node = objXmlDocument.SelectSingleNode("//User[Id='" + id + "']");

                    if (node != null)
                    {
                        objXmlDocument.ChildNodes[1].RemoveChild(node);
                    }
                    objXmlDocument.Save(fileName);

                    return true;
                }
                else
                {
                    Exception ex = new Exception("Database file does not exist in the folder");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("User cannot be deleted!", ex.InnerException);
            }
        }

        public IEnumerable<Models.User> GetAll()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    // Loading the file into XPath document
                    XPathDocument doc = new XPathDocument(fileName);
                    XPathNavigator nav = doc.CreateNavigator();

                    XPathExpression exp = nav.Compile("/Users/User"); // Getting all employees

                    XPathNodeIterator iterator = nav.Select(exp);
                    IList<User> objUsers = new List<User>();

                    while (iterator.MoveNext())
                    {
                        XPathNavigator nav2 = iterator.Current.Clone();

                        User objUser = new User();
                        objUser.Id = Convert.ToInt32(nav2.Select("//User").Current.SelectSingleNode("Id").InnerXml);
                        objUser.Username = nav2.Select("//User").Current.SelectSingleNode("Username").InnerXml;
                        objUser.Password = nav2.Select("//User").Current.SelectSingleNode("Password").InnerXml;
                        objUser.Firstname = nav2.Select("//User").Current.SelectSingleNode("Firstname").InnerXml;
                        objUser.Lastname = nav2.Select("//User").Current.SelectSingleNode("Lastname").InnerXml;
                        objUser.Mobile = nav2.Select("//User").Current.SelectSingleNode("Mobile").InnerXml;
                        objUser.Mail = nav2.Select("//User").Current.SelectSingleNode("Mail").InnerXml;
                        objUser.Role = nav2.Select("//User").Current.SelectSingleNode("Role").InnerXml;

                        objUsers.Add(objUser);
                    }
                    return objUsers;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Getting data from Users.xml has failed!",ex.InnerException);
            }
            return null;
        }

        public Models.User GetUserByUsername(string username)
        {
            try
            {
                User user = GetAll().First(u => u.Username.Equals(username));
                return user;
            }
            catch(Exception ex)
            {
                return null;
            }   
        }
    }
}
