using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NetView;

public class BoolToPortBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool configured = value is true;
        return configured
            ? new SolidColorBrush(Color.FromRgb(0xEA, 0xF3, 0xDE))  // green
            : new SolidColorBrush(Color.FromRgb(0xF5, 0xF4, 0xF0)); // gray
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class BoolToPortBorderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool configured = value is true;
        return configured
            ? new SolidColorBrush(Color.FromArgb(80, 0x3B, 0x6D, 0x11))  // green border
            : new SolidColorBrush(Color.FromArgb(30, 0, 0, 0));          // default
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class BoolToPortTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool configured = value is true;
        return configured
            ? new SolidColorBrush(Color.FromRgb(0x3B, 0x6D, 0x11))  // green text
            : new SolidColorBrush(Color.FromRgb(0x9C, 0x9A, 0x92)); // gray text
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class PortTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int portNum)
        {
            if (portNum > 48) return "SFP+";
            if (portNum > 32) return "2.5G";
            return "1G";
        }
        return "";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class PortTypeBadgeBgConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int portNum)
        {
            if (portNum > 48) return new SolidColorBrush(Color.FromRgb(0xE6, 0xF1, 0xFB)); // blue
            if (portNum > 32) return new SolidColorBrush(Color.FromRgb(0xFA, 0xEE, 0xDA)); // amber
            return new SolidColorBrush(Color.FromRgb(0xF5, 0xF4, 0xF0)); // gray
        }
        return Brushes.Transparent;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class PortTypeBadgeFgConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int portNum)
        {
            if (portNum > 48) return new SolidColorBrush(Color.FromRgb(0x18, 0x5F, 0xA5)); // blue
            if (portNum > 32) return new SolidColorBrush(Color.FromRgb(0x85, 0x4F, 0x0B)); // amber
            return new SolidColorBrush(Color.FromRgb(0x9C, 0x9A, 0x92)); // gray
        }
        return Brushes.Black;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class PortMarginTopConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int portNum && portNum == 49)
            return new Thickness(10, 0, 1, 1); // extra left gap before first SFP
        return new Thickness(1, 0, 1, 1);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class PortMarginBottomConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int portNum && portNum == 51)
            return new Thickness(10, 1, 1, 0); // extra left gap before first SFP
        return new Thickness(1, 1, 1, 0);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is true ? Visibility.Visible : Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
