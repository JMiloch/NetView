using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace NetView;

public partial class SwitchPort : ObservableObject
{
    public int PortNumber { get; set; }

    [ObservableProperty]
    private string _deviceName = "";

    [ObservableProperty]
    private string _ipAddress = "";

    [ObservableProperty]
    private string _vlan = "";

    [ObservableProperty]
    private string _function = "";

    [JsonIgnore]
    public bool IsConfigured => !string.IsNullOrWhiteSpace(DeviceName)
                             || !string.IsNullOrWhiteSpace(IpAddress)
                             || !string.IsNullOrWhiteSpace(Vlan)
                             || !string.IsNullOrWhiteSpace(Function);

    [JsonIgnore]
    public bool IsSfpPort => PortNumber > 48;

    public void NotifyConfigChanged() => OnPropertyChanged(nameof(IsConfigured));
}

public class SwitchConfig
{
    public string SwitchName { get; set; } = "USW Pro Max 48";
    public string Location { get; set; } = "";
    public List<PortData> Ports { get; set; } = [];
}

public class PortData
{
    public int PortNumber { get; set; }
    public string DeviceName { get; set; } = "";
    public string IpAddress { get; set; } = "";
    public string Vlan { get; set; } = "";
    public string Function { get; set; } = "";
}
