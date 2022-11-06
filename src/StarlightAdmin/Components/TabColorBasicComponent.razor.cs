
using System.Drawing;
using Microsoft.AspNetCore.Components;
using StarlightAdmin.Manager;

namespace StarlightAdmin.Components;

public partial class TabColorBasicComponent
{
    private IEnumerable<string> _colors = new List<string>();

    [Inject]
    IBluetoothManager btManager { get; set; } = default!;

    public TabColorBasicComponent()
    {
     
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() =>
        {
            _colors = generateColors();
            StateHasChanged();
        });
    }

    private async void ChangeColor(string newColor)
    {
        await btManager.SetColorAsync(newColor);
    }

    private IEnumerable<string> generateColors()
    {
        List<string> result = new();

        byte r = 255;
        byte g = 0;
        byte b = 0;
        byte step = 15;

        // g up
        for (int i = 0; i < 255; i += step)
        {
            g = (byte)i;
            result.Add("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        // r down
        for (int i = 255; i >= 0; i -= step)
        {
            r = (byte)i;
            result.Add("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        // b up
        for (int i = 0; i < 255; i += step)
        {
            b = (byte)i;
            result.Add("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        // g down
        for (int i = 255; i >= 0; i -= step)
        {
            g = (byte)i;
            result.Add("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        // r up
        for (int i = 0; i < 255; i += step)
        {
            r = (byte)i;
            result.Add("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        // b down
        for (int i = 255; i >= 0; i -= step)
        {
            b = (byte)i;
            result.Add("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        return result;
    }
}
