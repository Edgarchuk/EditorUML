using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
            EditAttributes = new Command(() => { EditSelect(_selectAttribute); });
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
        }


        private void EditSelect(FieldViewModel fieldViewModel)
        {
            FieldEdit fieldEdit = new FieldEdit();
            fieldEdit.DataContext = fieldViewModel;
            fieldEdit.Show();
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
        
        public ICommand MouseDown { get; }
        public ICommand MouseUp { get; }
        public ICommand MouseMove { get; }
        private FieldViewModel _selectAttribute;
        private FieldViewModel _selectMethod;
        public ICommand EditAttributes { get;  }
        public ICommand SelectionAttributeChanged { get;}
        public ICommand SelectionMethodChanged { get; }
        public ICommand AddAttribute { get;  }
        public ICommand AddMethod { get; }
        public ICommand DeleteAttribute { get; }
        public ICommand DeleteMethod { get; }
        
        private bool _mouseDownFlag = false;
    }

    static class Ext
    {
    }
}