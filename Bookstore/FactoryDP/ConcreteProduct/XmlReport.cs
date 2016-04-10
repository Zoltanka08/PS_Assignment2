using Bookstore.FactoryDP.AbstractProductInterface;
using Services.CustomException;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using XMLDatabase.Models;

namespace Bookstore.FactoryDP.ConcreteProduct
{
    public class XmlReport : IReport
    {
        private const string fileName = @"E:\University\AnIII\Sem II\PS\Lab\Assigment2\Bookstore\Bookstore\Report\XmlReport.xml";
        public void CreateReport(IBookService bookService)
        {
            IEnumerable<Book> books = bookService.GetAll().Where(b => b.Quantity <= 0);
            CreateRreport(fileName, books);
        }

        private void CreateRreport(string fileName, IEnumerable<Book> books)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            CreateXmlWrite(fileName);
            foreach (Book book in books)
            {
                Insert(book, fileName);
            }

        }

        private void CreateXmlWrite(string fileName)
        {
            XmlTextWriter textWrite = new XmlTextWriter(fileName, null);
            textWrite.WriteStartDocument();
            textWrite.WriteStartElement("Book");
            textWrite.WriteEndElement();
            textWrite.Close();
        }

        private bool Insert(Book book, string fileName)
        {
            try
            {
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
            catch (Exception ex)
            {
                throw new ReportException();
            }
        }
    }
}