using System.Collections.ObjectModel;
using System.Drawing;
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
    }
}