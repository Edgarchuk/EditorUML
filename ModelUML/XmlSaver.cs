using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace ModelUML
{
    public class XmlSaver
    {
        private XmlDocument _xmlDocument = new XmlDocument();
        private string _path;

        public XmlSaver(string path)
        {
            _path = path;
            XmlNode root = _xmlDocument.CreateElement("uml");
            _xmlDocument.AppendChild(root);
        }

        public void SetClasses(IEnumerable<Class> classes)
        {
            XmlNode root = _xmlDocument.DocumentElement;
            foreach (var i in classes)
            {
                XmlElement newClass = _xmlDocument.CreateElement("Class");
                AddAttribute(newClass,"Id", i.Id.ToString());
                AddAttribute(newClass,"Name",i.Name);
                AddAttribute(newClass,"Position", $"{i.Position.X} {i.Position.Y}");
                AddAttribute(newClass,"Size",$"{i.Size.X} {i.Size.Y}");
                root.AppendChild(newClass);
                foreach (var attribute in i.Attributes)
                {
                    XmlElement element = _xmlDocument.CreateElement("Attribute");
                    AddAttribute(element, "Name", attribute.Name);
                    AddAttribute(element, "Type", attribute.Type.ToString());
                    newClass.AppendChild(element);
                }
                foreach (var method in i.Methods)
                {
                    XmlElement element = _xmlDocument.CreateElement("Method");
                    AddAttribute(element, "Name", method.Name);
                    AddAttribute(element, "Type", method.Type.ToString());
                    newClass.AppendChild(element);
                }
            }
            _xmlDocument.Save(_path);
        }

        public void SetLines(IEnumerable<Line> lines)
        {
            XmlNode root = _xmlDocument.DocumentElement;
            foreach (var i in lines)
            {
                XmlElement newLine = _xmlDocument.CreateElement("Line");
                AddAttribute(newLine,"FirstId", i.First.ToString());
                AddAttribute(newLine,"SecondId", i.Second.ToString());
                root.AppendChild(newLine);
            }
            _xmlDocument.Save(_path);
            
        }

        private void AddAttribute(XmlElement newClass, string attribute, string value)
        {
            XmlAttribute xmlAttribute = _xmlDocument.CreateAttribute(attribute);
            xmlAttribute.Value = value;
            newClass.SetAttributeNode(xmlAttribute);
        }
    }

}