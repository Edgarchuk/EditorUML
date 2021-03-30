using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catel.MVVM;
using Microsoft.Win32;
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
                var att = new ObservableCollection<FieldViewModel>();
                att.Add(new FieldViewModel()
                {
                    Name = "New Attribute"
                });
                var met = new ObservableCollection<FieldViewModel>();
                met.Add(new FieldViewModel()
                {
                    Name = "New Method"
                });
                ClassViewModels.Add(new ClassViewModel()
                {
                    Id = ClassViewModels.GetMaxId() + 1,
                    Name = "Class",
                    Position = MainWindow.MousePosition,
                    Attributes = att,
                    Methods = met,
                    Height = 160,
                    Width = 200
                });
            });
            Load = new Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    XmlParser xmlParser = new XmlParser(openFileDialog.FileName);
                    ClassViewModels.Clear();
                    foreach (var classViewModel in xmlParser.GetClasses().ToClassViewModels())
                    {
                        ClassViewModels.Add(classViewModel);
                    }
                    LineaViewModels.Clear();
                    foreach (var line in xmlParser.GetLines())
                    {
                        LineaViewModels.Add(new LineViewModel(ClassViewModels.GetById(line.First), ClassViewModels.GetById(line.Second)));
                    }
                }
            });
            Save = new Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    XmlSaver xmlSaver = new XmlSaver(openFileDialog.FileName);
                    xmlSaver.SetClasses(ClassViewModels.ToClass());
                    xmlSaver.SetLines(LineaViewModels.ToLine());
                }
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
                var lineViewModel = new LineViewModel(_startAdd, end);
                LineaViewModels.Add(lineViewModel);
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

        public void DeleteClass(ClassViewModel delete)
        {
            for (int i = 0; i < LineaViewModels.Count; i++)
            {
                if (LineaViewModels[i].FirstClass == delete || LineaViewModels[i].SecondClass == delete)
                {
                    LineaViewModels.RemoveAt(i);
                    i--;
                }
            }

            ClassViewModels.Remove(delete);
        }
        public ICommand AddClass { get;}
        public ICommand Load { get; }

        public ICommand Save { get; }
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

        public static FieldViewModel ToFieldViewModel(this Field i)
        {
            return new FieldViewModel()
            {
                Name = i.Name,
                Type = i.Type
            };
        }

        public static IEnumerable<FieldViewModel> ToFieldViewModel(this IEnumerable<Field> fields)
        {
            foreach (var field in fields)
            {
                yield return field.ToFieldViewModel();
            }
        }

        public static ClassViewModel ToClassViewModel(this Class i)
        {
            return new ClassViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Position = new Point(i.Position.X, i.Position.Y),
                Width = i.Size.X,
                Height = i.Size.Y,
                Attributes = new ObservableCollection<FieldViewModel>( i.Attributes.ToFieldViewModel()),
                Methods = new ObservableCollection<FieldViewModel>(i.Methods.ToFieldViewModel())
            };
        }

        public static IEnumerable<ClassViewModel> ToClassViewModels(this IEnumerable<Class> i)
        {
            foreach (var qClass in i)
            {
                yield return qClass.ToClassViewModel();
            }
        }

        public static ClassViewModel GetById(this IEnumerable<ClassViewModel> classViewModels, int id)
        {
            foreach (var q in classViewModels)
            {
                if (q.Id == id)
                {
                    return q;
                }
            }

            return null;
        }

        public static Field ToField(this FieldViewModel fieldViewModel)
        {
            return new Field()
            {
                Type = fieldViewModel.Type,
                Name = fieldViewModel.Name
            };
        }

        public static IEnumerable<Field> ToField(this IEnumerable<FieldViewModel> fieldViewModels)
        {
            foreach (var fieldViewModel in fieldViewModels)
            {
                yield return fieldViewModel.ToField();
            }
        }

        public static Class ToClass(this ClassViewModel i)
        {
            return new Class()
            {
                Id = i.Id,
                Name = i.Name,
                Position = new System.Drawing.Point((int) i.Position.X, (int) i.Position.Y),
                Size = new  System.Drawing.Point((int) i.Width, (int) i.Height),
                Attributes = i.Attributes.ToField(),
                Methods = i.Methods.ToField()
            };
        }

        public static IEnumerable<Class> ToClass(this IEnumerable<ClassViewModel> i)
        {
            foreach (var classViewModel in i)
            {
                yield return classViewModel.ToClass();
            }
        }

        public static IEnumerable<Line> ToLine(this IEnumerable<LineViewModel> lineViewModels)
        {
            foreach (var lineViewModel in lineViewModels)
            {
                yield return new Line()
                {
                    First = lineViewModel.FirstClass.Id, Second = lineViewModel.SecondClass.Id
                };
            }
        }
    }
}