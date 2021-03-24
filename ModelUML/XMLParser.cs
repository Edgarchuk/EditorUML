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

        private XmlDocument _document;

        public XmlParser(string pathFile)
        {
            _document = new XmlDocument();
            _document.Load(pathFile);
        }

        public IEnumerable<Class> GetClasses()
        {
            foreach (XmlNode Node in _document.DocumentElement)
            {
                Point getPointString(string[] str)
                {
                    
                    var point = new Point();
                    point.X = int.Parse(str[0]);
                    point.Y = int.Parse(str[0]);
                    return point;
                }
                string[] position = Node.Attributes.GetNamedItem("Position").Value.Split();
                string[] size =  Node.Attributes.GetNamedItem("Size").Value.Split();
                if (Node.Name == "Class")
                {
                    Class temp = new Class()
                    {
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
                                    Type = xmlNode.Attributes.GetNamedItem("Type").Value.GetVisibilityType(),
                                };
                            }
                        }
                    }

                    yield return temp;
                }
            }
            yield break;
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