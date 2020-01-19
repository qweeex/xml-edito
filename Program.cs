using System;
using System.IO;
using System.Xml;

namespace XmlEdidor
{
    class Program
    {
        static string GetFile()
        {
            string document = "doc.xml";
            return document;
        }
        // Init script
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetFile());
            while (true)
            {
                int i = ShowMenu(doc);
                if (i == 0) break;
            }
        }
        static int ShowMenu(XmlDocument doc)
        {
            Console.WriteLine("\n 1: Просмотр содержимого \n 2: Добавление содержимого \n 3: Изменение содержимого \n 4: Удаление содержимого \n 0: Выход");
            Console.Write("Введите ваш выбор:");
            string str = Console.ReadLine();
            switch (str)
            {
                case "0": return 0; break;
                case "1": ShowBook(doc); return 1; break;
                case "2": AddBook(doc); return 1; break;
                case "3": ChangeBook(doc); return 1; break;
                case "4": DeleteBook(doc); return 1; break;
                default: Console.WriteLine("Введите одну из указанных цифр"); return 1; break;
            }
        }
        static void ShowBook(XmlDocument doc)
        {
            XmlNodeList list = doc.GetElementsByTagName("BOOK");
            int num = 1;
            string Head = String.Format("|{0,2}    |{1,40}     |{2,40}     |{3,30}     |{4,20}     |{5,10}", "ID", "Название", "Автор", "Обложка", "Кол-во страниц", "Цена");
            for (int i = 0; i < Head.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine(Head);
            for (int i = 0; i < Head.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            foreach (XmlNode node in list)
            {
                string table = String.Format("|{0,2}    |{1,40}     |{2,40}     |{3,30}     |{4,20}     |{5,10}", num++, node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText, node.ChildNodes[4].InnerText);
                Console.WriteLine(table);
                for (int i = 0; i < table.Length; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }
        }
        static void AddBook(XmlDocument doc)
        {
            XmlElement book = doc.CreateElement("BOOK");
            XmlElement title = doc.CreateElement("TITLE");
            Console.WriteLine("Введите название книги:");
            title.InnerText = Console.ReadLine();
            book.InsertAfter(title, book.LastChild);
            XmlElement author = doc.CreateElement("AUTHOR");
            Console.WriteLine("Введите автора книги:");
            author.InnerText = Console.ReadLine();
            book.InsertAfter(author, book.LastChild);
            XmlElement binding = doc.CreateElement("BINDING");
            Console.WriteLine("Введите тип обложки:");
            binding.InnerText = Console.ReadLine();
            book.InsertAfter(binding, book.LastChild);
            XmlElement pages = doc.CreateElement("PAGES");
            Console.WriteLine("Введите кол-во страниц:");
            pages.InnerText = Console.ReadLine();
            book.InsertAfter(pages, book.LastChild);
            XmlElement price = doc.CreateElement("PRICE");
            Console.WriteLine("Введите цену книги:");
            price.InnerText = Console.ReadLine();
            book.InsertAfter(price, book.LastChild);
            doc.DocumentElement.InsertAfter(book, doc.DocumentElement.LastChild);
            doc.Save(GetFile());
        }
        static void ChangeBook(XmlDocument doc)
        {
            Console.WriteLine("Введите номер изменяемой книги");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Что изменить: \n 1: Название \n 2: Автор \n 3: Тип обложки \n 4: Кол-во страниц \n 5: Цена");
            int c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите новое значение");
            string str = Console.ReadLine();
            doc.DocumentElement.ChildNodes[num - 1].ChildNodes[c - 1].InnerText = str;
            doc.Save(GetFile());
        }
        static void DeleteBook(XmlDocument doc)
        {
            Console.WriteLine("Введите номер удаляемой книги");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num < 0)
            {
                Console.WriteLine("Данного номера книги нет, попробуйте снова");
            }
            else
            {
                doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[num - 1]);
            }
        }

    }
}
