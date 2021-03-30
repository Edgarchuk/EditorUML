using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Xml;

namespace ModelUML
{
    public class XmlParser
    {
        private readonly string _pathFile;

        private XmlDocument _document;

        public XmlParser(string pathFile)
        {
            _pathFile = pathFile;
            _document = new XmlDocument();
        }

        public IEnumerable<Class> GetClasses()
        {
            _document.Load(_pathFile);
            foreach (XmlNode Node in _document.DocumentElement)
            {
                Point getPointString(string[] str)
                {
                    
                    var point = new Point();
                    point.X = int.Parse(str[0]);
                    point.Y = int.Parse(str[0]);
                    return point;
                }
                if (Node.Name == "Class")
                {
                    
                    string[] position = Node.Attributes.GetNamedItem("Position").Value.Split();
                    string[] size =  Node.Attributes.GetNamedItem("Size").Value.Split();
                    Class temp = new Class()
                    {
                        Id = Convert.ToInt32(Node.Attributes.GetNamedItem("Id").Value),
                        Name = Node.Attributes.GetNamedItem("Name").Value,
                        Attributes = GetField("Attribute"),
                        Methods = GetField("Method"),
                        Position = getPointString(position),
                        Size = getPointString(size)
                    };

                    IEnumerable<Field> GetField(string type)
                    {
                        foreach (XmlNode xmlNode in Node.ChildNodes)
                        {
                            if (xmlNode.Name == type)
                            {
                                yield return new Field()
                                {
                                    Name = xmlNode.Attributes.GetNamedItem("Name").Value,
                                    Type = Enum.Parse<VisibilityType>( xmlNode.Attributes.GetNamedItem("Type").Value),
                                };
                            }
                        }
                    }
                    yield return temp;

                }
            }
            yield break;
        }

        public IEnumerable<Line> GetLines()
        {
            foreach (XmlNode Node in _document.DocumentElement)
            {
                if (Node.Name == "Line")
                {
                    yield return new Line()
                    {
                        First = Convert.ToInt32(Node.Attributes.GetNamedItem("FirstId").Value),
                        Second = Convert.ToInt32(Node.Attributes.GetNamedItem("SecondId").Value)
                    };
                }
            }
        }
    }

    public static class StringExt
    {
        public static VisibilityType GetVisibilityType(this string str)
        {
            return (VisibilityType)int.Parse(str);
        }
    }
}