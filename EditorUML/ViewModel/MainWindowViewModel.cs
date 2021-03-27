using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catel.MVVM;
using ModelUML;

namespace EditorUML.ViewModel
{
    class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<ClassViewModel> ClassViewModels { get; set; }
        public ObservableCollection<LineViewModel> LineaViewModels { get; set; }
    
        public MainWindowViewModel()
        {
            ClassViewModels = new ObservableCollection<ClassViewModel>();
            ClassViewModels.Add(new ClassViewModel()
            {
                Name = "Test",
                Attributes = new ObservableCollection<FieldViewModel>()
                {
                    new FieldViewModel() {Name = "TestAt", Type = VisibilityType.Public}
                },
                Methods = new ObservableCollection<FieldViewModel>()
                {
                    new FieldViewModel() {Name = "TestMet", Type = VisibilityType.Private}
                },
                Id = 1,
                Position = new Point(100, 100),
                Height = 100,
                Width = 100
            });
            ClassViewModels.Add(new ClassViewModel()
            {
                Name = "Test",
                Attributes = new ObservableCollection<FieldViewModel>()
                {
                    new FieldViewModel() {Name = "TestAt", Type = VisibilityType.Public}
                },
                Methods = new ObservableCollection<FieldViewModel>()
                {
                    new FieldViewModel() {Name = "TestMet", Type = VisibilityType.Private}
                },
                Id = 1,
                Position = new Point(700, 100),
                Height = 100,
                Width = 100
            });

            LineaViewModels = new ObservableCollection<LineViewModel>();
            LineaViewModels.Add(new LineViewModel(ClassViewModels[0], ClassViewModels[1]));
        }

        
    }

}