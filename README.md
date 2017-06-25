# Saraff.Twain.Extensions
Saraff.Twain.NET Extensions (LINQ to TWAIN).

```c#
this.dsComboBox.Items.AddRange(
    this.Dsm.DataSources
        .Select(x => x.Identity.Name)
        .ToArray())
```

```c#
var _resolutions = this.Dsm
    .DataSources.ElementAtOrDefault(this.dsComboBox.SelectedIndex)?
    .GetCapability<float>(TwCap.XResolution).Values;

this.resolutionToolStripDropDownButton.DropDownItems.AddRange(
    _resolutions
        .Where(x => _resolutions.Count()<20 ? true : x.Value%100==0)
        .Select(x => new ToolStripMenuItem(
            string.Format("{0:N0} dpi",x.Value),
            null,
            this._ToolStripItemSelectedHandler) {Tag=x.Value} as ToolStripItem)
        .ToArray());
```

```c#
this.Dsm.DataSources.ElementAtOrDefault(this.dsComboBox.SelectedIndex)?.NativeTransfer(
    x => {
        this.pictureBox1.Image?.Dispose();
        this.pictureBox1.Image=x.Image;
    },
    null,
    null,
    x => this._ToLog(x.Exception));
```

To install Saraff.Twain.NET, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)
```
PM> Install-Package Saraff.Twain.Extensions
```

