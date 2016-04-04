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
            fileName = "Users.xml";
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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
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
                    IList<User> objEmployees = new List<User>();

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

                        objEmployees.Add(objUser);
                    }
                    return objEmployees;
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
            throw new NotImplementedException();
        }
    }
}
