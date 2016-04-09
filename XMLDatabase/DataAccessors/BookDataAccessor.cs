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
    public class BookDataAccessor : IBookDataAccessor
    {
        private string fileName;
        public BookDataAccessor()
        {
            fileName = "Books.xml";
        }

        public bool Insert(Book book)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    XmlTextWriter textWrite = new XmlTextWriter(fileName, null);
                    textWrite.WriteStartDocument();
                    textWrite.WriteStartElement("Book");
                    textWrite.WriteEndElement();
                    textWrite.Close();
                }

                // Create the XML docment by loading the file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                // Creating User node
                XmlElement subNode = xmlDoc.CreateElement("Book");

                // Getting the maximum Id based on the XML data already stored
                string strId = CommonMethods.GetMaxValue(xmlDoc, "Books" + "/" + "Book" + "/" + "Id").ToString();

                // Adding Id column. Auto generated column
                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Id", strId));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Title", book.Title));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Genre", book.Genre));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Description", book.Description));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Author", book.Author));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Pages", book.Pages.ToString()));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Published", book.Published));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Price", book.Price.ToString()));
                xmlDoc.DocumentElement.AppendChild(subNode);

                subNode.AppendChild(CommonMethods.CreateXMLElement(xmlDoc, "Quantity", book.Quantity.ToString()));
                xmlDoc.DocumentElement.AppendChild(subNode);

                // Saving the file after adding the new employee node
                xmlDoc.Save(fileName);

                return true;
            }
            catch(Exception ex)
            {
                throw new DatabaseException("Book cannot be inserted!", ex.InnerException);
            }
        }

        public bool Update(Models.Book book)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlDocument objXmlDocument = new XmlDocument();
                    objXmlDocument.Load(fileName);

                    XmlNode node = objXmlDocument.SelectSingleNode("//Book[Id='" + book.Id + "']");

                    if (node != null)
                    {
                        // Assigining all the values
                        node.SelectNodes("Id").Item(0).FirstChild.Value = book.Id.ToString();
                        node.SelectNodes("Title").Item(0).FirstChild.Value = book.Title;
                        node.SelectNodes("Genre").Item(0).FirstChild.Value = book.Genre;
                        node.SelectNodes("Description").Item(0).FirstChild.Value = book.Description;
                        node.SelectNodes("Author").Item(0).FirstChild.Value = book.Author;
                        node.SelectNodes("Pages").Item(0).FirstChild.Value = book.Pages.ToString();
                        node.SelectNodes("Published").Item(0).FirstChild.Value = book.Published;
                        node.SelectNodes("Price").Item(0).FirstChild.Value = book.Price.ToString();
                        node.SelectNodes("Quantity").Item(0).FirstChild.Value = book.Quantity.ToString();
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
                throw new DatabaseException("Book cannot be updated!", ex.InnerException);
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

                    XmlNode node = objXmlDocument.SelectSingleNode("//Book[Id='" + id + "']");

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
                throw new DatabaseException("Book cannot be deleted!", ex.InnerException);
            }
        }

        public IEnumerable<Models.Book> GetAll()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    // Loading the file into XPath document
                    XPathDocument doc = new XPathDocument(fileName);
                    XPathNavigator nav = doc.CreateNavigator();

                    XPathExpression exp = nav.Compile("/Books/Book"); // Getting all employees

                    XPathNodeIterator iterator = nav.Select(exp);
                    IList<Book> objBooks = new List<Book>();

                    while (iterator.MoveNext())
                    {
                        XPathNavigator nav2 = iterator.Current.Clone();

                        Book objBook = new Book();
                        objBook.Id = Convert.ToInt32(nav2.Select("//Book").Current.SelectSingleNode("Id").InnerXml);
                        objBook.Title = nav2.Select("//Book").Current.SelectSingleNode("Title").InnerXml;
                        objBook.Genre = nav2.Select("//Book").Current.SelectSingleNode("Genre").InnerXml;
                        objBook.Description = nav2.Select("//Book").Current.SelectSingleNode("Description").InnerXml;
                        objBook.Author = nav2.Select("//Book").Current.SelectSingleNode("Author").InnerXml;
                        objBook.Pages = Convert.ToInt32(nav2.Select("//Book").Current.SelectSingleNode("Pages").InnerXml);
                        objBook.Price = Convert.ToDouble(nav2.Select("//Book").Current.SelectSingleNode("Price").InnerXml);
                        objBook.Published = nav2.Select("//Book").Current.SelectSingleNode("Published").InnerXml;
                        objBook.Quantity = Convert.ToInt32(nav2.Select("//Book").Current.SelectSingleNode("Quantity").InnerXml);

                        objBooks.Add(objBook);
                    }
                    return objBooks;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Getting data from Users.xml has failed!",ex.InnerException);
            }
            return null;
        }



        public Book GetById(int id)
        {
            Book book = GetAll().First(b => b.Id == id);
            return book;
        }
    }
}
