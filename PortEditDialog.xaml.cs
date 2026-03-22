using System.Windows;
using System.Windows.Media;

namespace NetView;

public partial class PortEditDialog : Window
{
    public SwitchPort Port { get; }
    public bool Cleared { get; private set; }

    public string ResultDeviceName => TxtDeviceName.Text.Trim();
    public string ResultIpAddress => TxtIpAddress.Text.Trim();
    public string ResultVlan => TxtVlan.Text.Trim();
    public string ResultFunction => TxtFunction.Text.Trim();

    public PortEditDialog(SwitchPort port)
    {
        Port = port;
        InitializeComponent();

        HeaderText.Text = $"Port {port.PortNumber}";

        // Type badge
        string typeLabel;
        Brush badgeBg, badgeFg;
        if (port.PortNumber > 48)
        {
            typeLabel = "SFP+ 10G";
            badgeBg = new SolidColorBrush(Color.FromRgb(0xE6, 0xF1, 0xFB));
            badgeFg = new SolidColorBrush(Color.FromRgb(0x18, 0x5F, 0xA5));
        }
        else if (port.PortNumber > 32)
        {
            typeLabel = "2.5 GbE";
            badgeBg = new SolidColorBrush(Color.FromRgb(0xFA, 0xEE, 0xDA));
            badgeFg = new SolidColorBrush(Color.FromRgb(0x85, 0x4F, 0x0B));
        }
        else
        {
            typeLabel = "1 GbE";
            badgeBg = new SolidColorBrush(Color.FromRgb(0xF5, 0xF4, 0xF0));
            badgeFg = new SolidColorBrush(Color.FromRgb(0x9C, 0x9A, 0x92));
        }
        TypeBadge.Background = badgeBg;
        TypeText.Text = typeLabel;
        TypeText.Foreground = badgeFg;

        // Populate fields
        TxtDeviceName.Text = port.DeviceName;
        TxtIpAddress.Text = port.IpAddress;
        TxtVlan.Text = port.Vlan;
        TxtFunction.Text = port.Function;

        TxtDeviceName.Focus();
        TxtDeviceName.SelectAll();
    }

    private void SaveClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void ClearClick(object sender, RoutedEventArgs e)
    {
        TxtDeviceName.Text = "";
        TxtIpAddress.Text = "";
        TxtVlan.Text = "";
        TxtFunction.Text = "";
        Cleared = true;
        DialogResult = true;
    }
}
