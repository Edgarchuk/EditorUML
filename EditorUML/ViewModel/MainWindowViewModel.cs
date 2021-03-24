using System.Collections.ObjectModel;
using System.Windows;
using ModelUML;

namespace EditorUML.ViewModel
{
    class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<ClassViewModel> ClassViewModels { get; set; }

        public MainWindowViewModel()
        {
            ClassViewModels = new ObservableCollection<ClassViewModel>();
            ClassViewModels.Add(new ClassViewModel()
            {
                Name = "Test",
                Attributes = new ObservableCollection<FieldViewModel>()
                {
                    new FieldViewModel(){Name = "TestAtttttttttttttttttttttttttttttttttttttttttt", Type = VisibilityType.Public}
                },
                Methods = new ObservableCollection<FieldViewModel>()
                {
                    new FieldViewModel() {Name = "TestMet", Type = VisibilityType.Private}
                },
                Id = 1,
                Position = new Point(100, 100),
                Size = new Point(100, 100)
            });
        }
    }
}