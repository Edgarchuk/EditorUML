using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Numerics;

namespace ModelUML
{
    public enum VisibilityType
    {
        Private,
        Protected,
        Public
    }
    public class Field
    {
        public string Name = "";
        public VisibilityType Type;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    } 
    public class Class
    {
        public string Name = "";
        public IEnumerable<Field> Attributes;
        public IEnumerable<Field> Methods;
        public Point Position;
        public Point Size;
        public int Id;

        public override string ToString()
        {
            string temp = $"Class {Id} {Position} {Size}" + Name + " Method = {";
            foreach (var method in Methods)
            {
                temp += method.ToString() + " ";
            }

            temp += "} Attributes = {";
            foreach (var attribute in Attributes)
            {
                temp += attribute.ToString() + " ";
            }

            temp += "}";
            return temp;
        }
    }

    public class Line
    {
        public int First;
        public int Second;

        public override string ToString()
        {
            return $"Line {First} {Second}";
        }
    }
}