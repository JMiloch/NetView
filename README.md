# NetView

A visual switch port manager for Ubiquiti USW Pro Max 48. Configure, document, and export your network switch port assignments with a clean, minimal UI.

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4) ![Windows](https://img.shields.io/badge/Platform-Windows-0078D6) ![License](https://img.shields.io/badge/License-MIT-green)

## Features

- **Visual port map** — 52 ports displayed in the same layout as the physical switch (2 rows + SFP+)
- **Click to configure** — Device name, IP address, VLAN, and function per port
- **Hover tooltips** — Quick overview of port details on mouseover
- **Color-coded status** — Green for configured ports, gray for empty
- **Port type badges** — 1 GbE (1-32), 2.5 GbE (33-48), SFP+ 10G (49-52)
- **Configured ports table** — Sortable overview of all assigned ports
- **CSV export** — Export full port configuration for documentation
- **Auto-save** — Configuration persisted to `%AppData%\NetView\netview-config.json`

## Download

Grab the latest `NetView.exe` from the [Releases](../../releases) page. No installation required — single self-contained executable.

## Build from source

```bash
dotnet build
dotnet run
```

### Publish

```bash
dotnet publish -c Release -o publish
```

## Requirements

- Windows 10/11 (x64)
- .NET 9.0 (included in self-contained build)

## License

MIT
