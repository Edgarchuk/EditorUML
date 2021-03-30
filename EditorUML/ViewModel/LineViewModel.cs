using System;
using System.ComponentModel;
using System.Numerics;
using System.Windows;
using Vector = System.Windows.Vector;

namespace EditorUML.ViewModel
{
    internal class LineViewModel : ViewModel
    {
        private readonly ClassViewModel _firstClass;

        public ClassViewModel FirstClass => _firstClass;

        public ClassViewModel SecondClass => _secondClass;

        private readonly ClassViewModel _secondClass;

        public LineViewModel(ClassViewModel firstClass, ClassViewModel secondClass)
        {
            _firstClass = firstClass;
            _secondClass = secondClass;

            firstClass.PropertyChanged += ClassOnPropertyChanged;
            secondClass.PropertyChanged += ClassOnPropertyChanged;
        }

        private void ClassOnPropertyChanged(object sender,PropertyChangedEventArgs args)
        {
            if (args.PropertyName != "Position" && args.PropertyName != "Height" &&
                args.PropertyName != "Width") return;

            var X1 = First.X;
            var Y1 = First.Y;
            var X2 = Second.X;
            var Y2 = Second.Y;
            
            double X3 = (X1 + X2) / 2;
            double Y3 = (Y1 + Y2) / 2;
 
            // длина отрезка
            double d = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
 
            // координаты вектора
            double X = X2 - X1;
            double Y = Y2 - Y1;
 
            // координаты точки, удалённой от центра к началу отрезка на 10px
            double X4 = X3 - (X / d) * 10;
            double Y4 = Y3 - (Y / d) * 10;
 
            // из уравнения прямой { (x - x1)/(x1 - x2) = (y - y1)/(y1 - y2) } получаем вектор перпендикуляра
            // (x - x1)/(x1 - x2) = (y - y1)/(y1 - y2) =>
            // (x - x1)*(y1 - y2) = (y - y1)*(x1 - x2) =>
            // (x - x1)*(y1 - y2) - (y - y1)*(x1 - x2) = 0 =>
            // полученные множители x и y => координаты вектора перпендикуляра
            double Xp = Y2 - Y1;
            double Yp = X1 - X2;
 
            // координаты перпендикуляров, удалённой от точки X4;Y4 на 5px в разные стороны
            double X5 = X4 + (Xp / d) * 5;
            double Y5 = Y4 + (Yp / d) * 5;
            double X6 = X4 - (Xp / d) * 5;
            double Y6 = Y4 - (Yp / d) * 5;

            ArrowFirst = new Point(X5, Y5);
            ArrowMiddle = new Point(X3, Y3);
            ArrowSecond = new Point(X6, Y6);
            
            OnPropertyChanged("First");
            OnPropertyChanged("Second");
            OnPropertyChanged("ArrowFirst");
            OnPropertyChanged("ArrowMiddle");
            OnPropertyChanged("ArrowSecond");
        }

        public Point First
        {
            get => ClassPosition(_firstClass);
        }

        public Point Second
        {
            get =>  ClassPosition(_secondClass);
        }

        public Point ArrowFirst { get; set; }
        public Point ArrowSecond { get; set; }
        public Point ArrowMiddle { get; set; }

        private Point ClassPosition(ClassViewModel classViewModel)
        {
            var vector = new Vector( classViewModel.Width/2,  classViewModel.Height / 2);
            var position = classViewModel.Position + vector;
            return position;
        }
    }

}