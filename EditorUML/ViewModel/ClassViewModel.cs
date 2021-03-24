using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Catel.MVVM;
using ModelUML;

namespace EditorUML.ViewModel
{
    class ClassViewModel : ViewModel
    {
        private string _name;
        private ObservableCollection<FieldViewModel> _attributes;
        private ObservableCollection<FieldViewModel> _methods;
        private Point _position;
        private Point _size;
        private int _id;

        private System.Windows.Point _lastPositionMouse = new System.Windows.Point();
        

        public ClassViewModel()
        {
            MouseMove = new Command<MouseEventArgs>((MouseEventArgs e) =>
            {
                if (_mouseDownFlag == false) return;
                if (_lastPositionMouse == null) _lastPositionMouse = MainWindow.MousePosition; 
                Position += (MainWindow.MousePosition - _lastPositionMouse);
                _lastPositionMouse = MainWindow.MousePosition;

            });
            MouseUp = new Command(() =>
            {
                _mouseDownFlag = false;
            });
            MouseDown = new Command(() =>
            {
                _lastPositionMouse = MainWindow.MousePosition;
                _mouseDownFlag = true;
            });
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

        public Point Size
        {
            get => _size;
            set => Set(ref _size, value);
        }

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
        
        public Command MouseDown { get; }
        public Command MouseUp { get; }
        
        public Command<MouseEventArgs> MouseMove { get; }
        

        private bool _mouseDownFlag = false;
    }

    static class Ext
    {
    }
}