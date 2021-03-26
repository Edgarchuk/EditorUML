using System.Windows;

namespace EditorUML.ViewModel
{
    class LineViewModel : ViewModel
    {
        public ClassViewModel FirstClass { get; set; }
        public ClassViewModel SecondClass { get; set; }

        public Point First
        {
            get => ClassPosition(FirstClass);
        }

        public Point Second
        {
            get =>  ClassPosition(SecondClass);
        }

        private Point ClassPosition(ClassViewModel classViewModel)
        {
            return classViewModel.Position + new Vector( classViewModel.Size.X/2,  classViewModel.Size.Y / 2);
        }
    }
}