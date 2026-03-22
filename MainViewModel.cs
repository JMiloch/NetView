using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace NetView;

public partial class MainViewModel : ObservableObject
{
    private static readonly string ConfigDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NetView");

    private static readonly string ConfigPath = Path.Combine(ConfigDir, "netview-config.json");

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true
    };

    public ObservableCollection<SwitchPort> Ports { get; } = [];

    // Top row: odd ports 1,3,5,...47 + SFP 49,50
    public ObservableCollection<SwitchPort> TopRow { get; } = [];
    // Bottom row: even ports 2,4,6,...48 + SFP 51,52
    public ObservableCollection<SwitchPort> BottomRow { get; } = [];

    [ObservableProperty]
    private string _switchName = "USW Pro Max 48";

    [ObservableProperty]
    private string _location = "";

    [ObservableProperty]
    private int _configuredCount;

    [ObservableProperty]
    private int _totalPorts = 52;

    [ObservableProperty]
    private string _statusText = "";

    public MainViewModel()
    {
        InitializePorts();
        LoadConfig();
        UpdateStats();
    }

    private void InitializePorts()
    {
        for (int i = 1; i <= 52; i++)
        {
            var port = new SwitchPort { PortNumber = i };
            Ports.Add(port);
        }

        // Top row: odd RJ45 (1,3,5...47) + SFP 49,50
        for (int i = 1; i <= 47; i += 2)
            TopRow.Add(Ports[i - 1]);
        TopRow.Add(Ports[48]); // Port 49
        TopRow.Add(Ports[49]); // Port 50

        // Bottom row: even RJ45 (2,4,6...48) + SFP 51,52
        for (int i = 2; i <= 48; i += 2)
            BottomRow.Add(Ports[i - 1]);
        BottomRow.Add(Ports[50]); // Port 51
        BottomRow.Add(Ports[51]); // Port 52
    }

    public void UpdateStats()
    {
        ConfiguredCount = Ports.Count(p => p.IsConfigured);
        StatusText = $"{ConfiguredCount} / {TotalPorts} Ports configured";
    }

    public void SaveConfig()
    {
        var config = new SwitchConfig
        {
            SwitchName = SwitchName,
            Location = Location,
            Ports = Ports.Where(p => p.IsConfigured).Select(p => new PortData
            {
                PortNumber = p.PortNumber,
                DeviceName = p.DeviceName,
                IpAddress = p.IpAddress,
                Vlan = p.Vlan,
                Function = p.Function
            }).ToList()
        };

        Directory.CreateDirectory(ConfigDir);
        var json = JsonSerializer.Serialize(config, JsonOpts);
        File.WriteAllText(ConfigPath, json);
    }

    private void LoadConfig()
    {
        if (!File.Exists(ConfigPath)) return;

        try
        {
            var json = File.ReadAllText(ConfigPath);
            var config = JsonSerializer.Deserialize<SwitchConfig>(json);
            if (config == null) return;

            SwitchName = config.SwitchName;
            Location = config.Location;

            foreach (var pd in config.Ports)
            {
                var port = Ports.FirstOrDefault(p => p.PortNumber == pd.PortNumber);
                if (port == null) continue;
                port.DeviceName = pd.DeviceName;
                port.IpAddress = pd.IpAddress;
                port.Vlan = pd.Vlan;
                port.Function = pd.Function;
                port.NotifyConfigChanged();
            }
        }
        catch
        {
            // Ignore corrupt config
        }
    }

    [RelayCommand]
    private void ClearPort(SwitchPort port)
    {
        port.DeviceName = "";
        port.IpAddress = "";
        port.Vlan = "";
        port.Function = "";
        port.NotifyConfigChanged();
        UpdateStats();
        SaveConfig();
    }

    [RelayCommand]
    private void ExportCsv()
    {
        var dialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "CSV|*.csv",
            FileName = $"NetView-{SwitchName.Replace(" ", "_")}.csv"
        };
        if (dialog.ShowDialog() != true) return;

        var lines = new List<string> { "Port;Type;Device;IP;VLAN;Function" };
        foreach (var p in Ports)
        {
            var typ = p.IsSfpPort ? "SFP+" : "RJ45";
            lines.Add($"{p.PortNumber};{typ};{p.DeviceName};{p.IpAddress};{p.Vlan};{p.Function}");
        }
        File.WriteAllLines(dialog.FileName, lines);
    }
}
