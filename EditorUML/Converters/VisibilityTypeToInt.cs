using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using Catel.MVVM.Converters;
using ModelUML;

namespace EditorUML.Converters
{
    [ValueConversion(typeof(VisibilityType), typeof(int), ParameterType = typeof(VisibilityType))]
    public class VisibilityTypeToInt : ValueConverterBase<VisibilityType>
    {
        protected override object Convert(VisibilityType value, Type targetType, object parameter)
        {
            VisibilityType visibilityType = VisibilityType.Private;
            if (parameter != null)
            {
                if (parameter is VisibilityType) visibilityType = (VisibilityType) parameter;
            }
            
            return (int) visibilityType;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter)
        {
            VisibilityType visibilityType = VisibilityType.Private;
            if (parameter != null)
            {
                if (parameter is VisibilityType) visibilityType = (VisibilityType) parameter;
            }
            
            bool isChecked = false;
            if (value is bool)
            {
                isChecked = (bool)value;
            }
            else if (value is bool?)
            {
                isChecked = ((bool?)value).HasValue ? ((bool?)value).Value : false;
            }
            
            return (isChecked) ? visibilityType : Binding.DoNothing;
        }
    }
}