using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catel.MVVM;
using ModelUML;
using Vector = System.Windows.Vector;

namespace EditorUML.ViewModel
{
    public enum ResizeDirection
    {   
        Left,
        Up,
        Right,
        Down,
        Nothing
    }

    public class ClassViewModel : ViewModel
    {
        private string _name;
        private ObservableCollection<FieldViewModel> _attributes;
        private ObservableCollection<FieldViewModel> _methods;
        private Point _position;
        private double _width = 0;
        private double _height = 0;
        private int _id;

        private ResizeDirection _resizeDirection;
        private System.Windows.Point _lastPositionMouse = new System.Windows.Point();
        
        private const int ThicknessResize = 10;

        

        public ClassViewModel()
        {
            MouseMove = new Command(() => {});
            MouseUp = new Command(() =>
            {
                _mouseDownFlag = false;
                MouseMove = new Command(() => { });
                OnPropertyChanged("MouseMove");
            });
            MouseDown = new Command(() =>
            {
                MainWindowViewModel.MainWindowSingleton.EndNewLine(this);
                MainWindowViewModel.MainWindowSingleton.EndDeleteLine(this);
                var mousePosition = MainWindow.MousePosition - Position;
                _resizeDirection = mousePosition.OnBorder(new Size(_width, _height), ThicknessResize);
                _lastPositionMouse = MainWindow.MousePosition;
                if (_resizeDirection == ResizeDirection.Nothing)
                {
                    _mouseDownFlag = true;
                    MouseMove = new Command(() =>
                    {
                        if (_mouseDownFlag == false) return;
                        Position += (MainWindow.MousePosition - _lastPositionMouse);
                        _lastPositionMouse = MainWindow.MousePosition;

                    });
                }
                else
                {
                    MouseMove = new Command(ResizeMove);
                }
                OnPropertyChanged("MouseMove");
            });
            EditAttributes = new Command(() => { EditSelect(_selectAttribute); });
            EditMethods = new Command(() => { EditSelect(_selectMethod); });
            SelectionAttributeChanged = new Command<RoutedEventArgs>((RoutedEventArgs e) =>
            {
                ListView listView = (ListView) e.Source;
                if (listView.SelectedItem == null) return;
                _selectAttribute = (FieldViewModel) listView.SelectedItem;
            });
            SelectionMethodChanged = new Command<RoutedEventArgs>((RoutedEventArgs e) =>
            {
                ListView listView = (ListView) e.Source;
                if (listView.SelectedItem == null) return;
                _selectMethod = (FieldViewModel) listView.SelectedItem;
            });
            AddAttribute = new Command(() =>
            {
                FieldViewModel fieldViewModel = new FieldViewModel();
                Attributes.Add(fieldViewModel);
                EditSelect(fieldViewModel);
            });
            AddMethod = new Command(() =>
            {
                FieldViewModel fieldViewModel = new FieldViewModel();
                Methods.Add(fieldViewModel);
                EditSelect(fieldViewModel);
            });
            DeleteAttribute = new Command(() =>
            {
                Attributes.Remove(_selectAttribute);
                _selectAttribute = null;
            });
            DeleteMethod = new Command(() =>
            {
                Methods.Remove(_selectMethod);
                _selectMethod = null;
            });
            AddLine = new Command(() =>
            {
                MainWindowViewModel.MainWindowSingleton.StartNewLine(this);
            });
            DeleteLine = new Command(() =>
            {
                MainWindowViewModel.MainWindowSingleton.StartDeleteLine(this);
            });
            DeleteClass = new Command(() =>
            {
                MainWindowViewModel.MainWindowSingleton.DeleteClass(this);
                ;
            });
        }


        private void EditSelect(FieldViewModel fieldViewModel)
        {
            FieldEdit fieldEdit = new FieldEdit();
            fieldEdit.DataContext = fieldViewModel;
            fieldEdit.Show();
        }

        private void ResizeTo(double width, double height)
        {
            Width += width;
            Height += height;
        }

        private void MoveTo(int x, int y)
        {
            var position = Position;
            position.X += x;
            position.Y += y;
            Position = position;
        }

        private void ResizeMove()
        {
            var position = MainWindow.MousePosition;
            var deltaPosition = position - _lastPositionMouse;
            if (_resizeDirection == ResizeDirection.Left)
            {
                MoveTo((int) deltaPosition.X, 0);
                ResizeTo(-deltaPosition.X, 0);
            }

            ;
            if (_resizeDirection == ResizeDirection.Up)
            {
                MoveTo(0, (int) deltaPosition.Y);
                ResizeTo(0, -deltaPosition.Y);
            }

            ;
            if (_resizeDirection == ResizeDirection.Right) ResizeTo(deltaPosition.X, 0);
            if (_resizeDirection == ResizeDirection.Down) ResizeTo(0,deltaPosition.Y);
            _lastPositionMouse = position;
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public ObservableCollection<FieldViewModel> Attributes
        {
            get => _attributes;
            set => Set(ref _attributes, value);
        }
        public ObservableCollection<FieldViewModel> Methods
        {
            get => _methods;
            set => Set(ref _methods, value);
        }
        public Point Position
        {
            get => _position;
            set => Set(ref _position, value);
        }

        public double Width
        {
            get => (!double.IsNaN(_width)) ? _width : 0;
            set => Set(ref _width, value);
        }

        public double Height
        {
            get => (!double.IsNaN(_height)) ? _height : 0;
            set => Set(ref _height, value);
        }

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
        
        public ICommand MouseDown { get; }
        public ICommand MouseUp { get; }
        public ICommand MouseMove { get; set; }
        private FieldViewModel _selectAttribute;
        private FieldViewModel _selectMethod;
        public ICommand EditAttributes { get;  }
        public ICommand SelectionAttributeChanged { get;}
        public ICommand SelectionMethodChanged { get; }
        public ICommand AddAttribute { get;  }
        public ICommand AddMethod { get; }
        public ICommand DeleteAttribute { get; }
        public ICommand DeleteMethod { get; }
        public ICommand AddLine { get; }
        public ICommand DeleteLine { get; }

        public ICommand DeleteClass { get; }

        public object EditMethods { get; }

        private bool _mouseDownFlag = false;
        private bool _resizeFlag = false;
    }
    public static class ExtMethods
    {
        public static ResizeDirection OnBorder(this Vector point, Size size, int borderThickness)
        {
            if (point.X < borderThickness) return ResizeDirection.Left;
            if (point.Y < borderThickness) return ResizeDirection.Up;
            if (point.X > size.Width - borderThickness) return ResizeDirection.Right;
            if (point.Y > size.Height - borderThickness) return ResizeDirection.Down;
            return ResizeDirection.Nothing;
        }
    }
}