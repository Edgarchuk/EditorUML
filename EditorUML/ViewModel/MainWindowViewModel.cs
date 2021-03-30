using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catel.MVVM;
using ModelUML;

namespace EditorUML.ViewModel
{
    class MainWindowViewModel : ViewModel
    {
        public static MainWindowViewModel MainWindowSingleton;
        public ObservableCollection<ClassViewModel> ClassViewModels { get; set; }
        public ObservableCollection<LineViewModel> LineaViewModels { get; set; }
    
        public MainWindowViewModel()
        {
            MainWindowSingleton = this;
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
                Id = 0,
                Position = new Point(100, 100),
                Height = 160,
                Width = 200
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
                Height = 160,
                Width = 200
            });

            LineaViewModels = new ObservableCollection<LineViewModel>();
            LineaViewModels.Add(new LineViewModel(ClassViewModels[0], ClassViewModels[1]));

            AddClass = new Command(() =>
            {
                ClassViewModels.Add(new ClassViewModel()
                {
                    Id = ClassViewModels.GetMaxId() + 1,
                    Name = "Class",
                    Position = MainWindow.MousePosition,
                    Attributes = new ObservableCollection<FieldViewModel>(),
                    Methods = new ObservableCollection<FieldViewModel>(),
                    Height = 160,
                    Width = 200
                });
            });
        }

        private ClassViewModel _startAdd;
        public void StartNewLine(ClassViewModel start)
        {
            _startAdd = start;
        }

        public void EndNewLine(ClassViewModel end)
        {
            if (_startAdd != null && end != _startAdd)
            {
                LineaViewModels.Add(new LineViewModel(_startAdd, end));
            }

            _startAdd = null;
        }

        private ClassViewModel _startDelete;
        public void StartDeleteLine(ClassViewModel start)
        {
            _startDelete = start;
        }

        public void EndDeleteLine(ClassViewModel end)
        {
            if (_startDelete != null && end != _startDelete)
            {
                for (int j = 0; j < LineaViewModels.Count; j++)
                {
                    if (LineaViewModels[j].FirstClass == _startDelete && LineaViewModels[j].SecondClass == end)
                    {
                        LineaViewModels.RemoveAt(j);
                    }
                }
            }

            _startDelete = null;
        }
        public ICommand AddClass { get;}
    }

    public static class ExtCollection
    {
        public static int GetMaxId(this IEnumerable<ClassViewModel> classViewModels)
        {
            int max = 0;
            foreach (var viewModel in classViewModels)
            {
                if (max < viewModel.Id)
                {
                    max = viewModel.Id;
                }
            }

            return max;
        }
    }
}