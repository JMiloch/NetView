using System.Windows;
using System.Windows.Input;

namespace NetView;

public partial class MainWindow : Window
{
    private readonly MainViewModel _vm;

    public MainWindow()
    {
        InitializeComponent();
        _vm = new MainViewModel();
        DataContext = _vm;
        RefreshTable();
    }

    private void Port_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement fe && fe.Tag is SwitchPort port)
        {
            var dlg = new PortEditDialog(port) { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                port.DeviceName = dlg.ResultDeviceName;
                port.IpAddress = dlg.ResultIpAddress;
                port.Vlan = dlg.ResultVlan;
                port.Function = dlg.ResultFunction;
                port.NotifyConfigChanged();
                _vm.UpdateStats();
                _vm.SaveConfig();
                RefreshTable();
            }
        }
    }

    private void ExportCsv_Click(object sender, RoutedEventArgs e)
    {
        _vm.ExportCsvCommand.Execute(null);
    }

    private void RefreshTable()
    {
        var configured = _vm.Ports.Where(p => p.IsConfigured).OrderBy(p => p.PortNumber).ToList();
        PortTable.ItemsSource = configured;
        EmptyStateText.Visibility = configured.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}
