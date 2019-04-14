using System;
using System.Collections.Generic;
using System.Xml;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Инициализируем объекта типа XmlTextReader и загружаем XML документ с сайта центрального банка
            XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp");

            //В переменную SomeCurrencies будем сохранять куски XML которые, находятся между тегами Valute
            string SomeCurrencies = "";
            // Создаём список в котором будет храниться информация о валютах в строковом типе данных
            List<string> Valute = new List<string>();

            //Перебираем все узлы в загруженном документе
            while (reader.Read())
            {
                //Проверяем тип текущего узла
                switch (reader.NodeType)
                {
                    //Если этого элемент Valute, то добавляем его в список Valute
                    case XmlNodeType.Element:

                        if (reader.Name == "Valute")
                        {
                            SomeCurrencies = reader.ReadOuterXml();
                            Valute.Add(SomeCurrencies);
                        }
                        break;
                }
            }

            decimal MaxValue = 0;
            string MaxCurName = "";
            decimal MinValue = 200;
            string MinCurName = "";

            // Перебираем в цикле все строки списка Valute, чтобы найти максимальное значение стоимости валюты в рублях и её наименование
            foreach (var cur in Valute)
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.LoadXml(cur);
                XmlNode xmlNode = XmlDocument.SelectSingleNode("Valute/Value");
                XmlNode xmlNode1 = XmlDocument.SelectSingleNode("Valute/Name");
                decimal Value = Convert.ToDecimal(xmlNode.InnerText);
                string Name = Convert.ToString(xmlNode1.InnerText);
                if (Value > MaxValue)
                {
                    MaxValue = Value;
                    MaxCurName = Name;
                }

            }
            Console.WriteLine($"{MaxCurName} -- {MaxValue}");

            // Перебираем в цикле все строки списка Valute, чтобы найти минимальное значение стоимости валюты в рублях и её наименование
            foreach (var cur in Valute)
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.LoadXml(cur);
                XmlNode xmlNode = XmlDocument.SelectSingleNode("Valute/Value");
                XmlNode xmlNode1 = XmlDocument.SelectSingleNode("Valute/Name");
                decimal Value = Convert.ToDecimal(xmlNode.InnerText);
                string Name = Convert.ToString(xmlNode1.InnerText);
                if (Value < MinValue)
                {
                    MinValue = Value;
                    MinCurName = Name;
                }

            }
            Console.WriteLine($"{MinCurName} -- {MinValue}");
            Console.ReadKey();
        }
    }
}
