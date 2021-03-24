using ModelUML;

namespace EditorUML.ViewModel
{
    public class FieldViewModel : ViewModel
    {
        private string _name ;
        private VisibilityType _type;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public VisibilityType Type
        {
            get => _type;
            set => Set(ref _type, value);
        }
    }
}