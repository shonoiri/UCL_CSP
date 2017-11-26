using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Text.RegularExpressions;

namespace EX_01
{
    //This method helps to catch ultiple errors
    [ValueConversion(typeof(ReadOnlyObservableCollection<ValidationError>), typeof(string))]
    public class ValidationErrorsToStringConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ValidationErrorsToStringConverter();
        }


        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ReadOnlyObservableCollection<ValidationError> errors =
                value as ReadOnlyObservableCollection<ValidationError>;

            if (errors == null)
            {
                return string.Empty;
            }

            return string.Join("\n", (from e in errors select e.ErrorContent as string).ToArray());
        }

    }

    /*
     * This class represents validator for user input data. 
     */
    public class DataValidator : ValidationRule
    {
        //Type of file to validate : Name, Code ... etc
        public string DataType { get; set; }

        //Simple field validation
        public override ValidationResult Validate
          (object value, System.Globalization.CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);
            string codePattern = "[a-z\\._:]+";
            string msg = null;
            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, $"Value cannot be empty.");

            switch (DataType)
            {
                case "Name":
                    if (value.ToString().Length < 2)
                        msg = "Name cannot be less than 2 characters long.";
                    else if (value.ToString().Length > 100)
                        msg = "Name cannot be more than 100 characters long.";
                    break;

                case "Code":
                   if (Regex.Matches(value.ToString(), codePattern, RegexOptions.IgnoreCase).Count != 1)
                        msg = "Code should contain only characters : latin letters, dot or underscore";
                    else if (value.ToString().Length < 5)
                        msg = "Code cannot be less than 5 characters long.";
                    else if (value.ToString().Length > 50)
                        msg = "Code cannot be more than 50 characters long.";
                    else if (MainWindow.GetMeetingCenterByCode(value.ToString()) != null
                         || MainWindow.GetMeetingRoomByCode(value.ToString()) != null)
                        msg = "Code must be unique.";
                    break;

                case "Description":
                    if (value.ToString().Length < 10)
                        msg = "Description cannot be less than 10 characters long.";
                    else if (value.ToString().Length > 300)
                        msg = "Description cannot be more than 300 characters long.";
                    break;
                case "Capacity":
                    try
                    {
                        Int32.Parse(value.ToString());
                    }
                    catch
                    {
                        msg = "Capacity must be an integer in range 1..100";
                    }
                    break;
                default: throw new InvalidCastException($"{DataType} is not supported");
            }

            if (msg != null)
                return new ValidationResult(false, msg);
            else
                return new ValidationResult(true, msg);
        }

    }
}
