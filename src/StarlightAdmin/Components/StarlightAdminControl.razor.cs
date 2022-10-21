using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using StarlightAdmin.Manager;

namespace StarlightAdmin.Components;

public partial class StarlightAdminControl
{
    private int currentBrightness = 100;
    private MudColor currentColor = new MudColor(255, 0, 0, 255);
    private int currentAnimationModeIndex = 0;

    public bool IsConnected => btManager.IsConnected;

    public int CurrentTabIndex { get; set; } = 0;

    private async Task ConnectWithDevice()
    {
        while (!btManager.IsConnected)
        {
            await btManager.ConnectWithDeviceAsync();
        }

        currentBrightness = await btManager.GetBrightnessAsync();
        currentColor = new(await btManager.GetColorAsync());
        StateHasChanged();
    }

    private async Task UpdateBrightness(int newValue)
    {
        currentBrightness = newValue;
        await btManager.SetBrightnessAsync(currentBrightness);
    }

    private async Task UpdateColor(MudColor newColor)
    {
        currentColor = newColor;
        await btManager.SetColorAsync(currentColor.Value.Substring(0, currentColor.Value.Length - 2));

    }

    private async Task UpdateAnimationModeIndex(int index)
    {
        currentAnimationModeIndex = index;
        await btManager.SetAnimationModeIndexAsync(index);
    }

    [Inject]
    IBluetoothManager btManager { get; set; } = default!;
}
