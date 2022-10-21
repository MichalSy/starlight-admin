namespace StarlightAdmin.Manager;

public interface IBluetoothManager
{
    bool IsConnected { get; }

    Task ConnectWithDeviceAsync();
    Task<int> GetBrightnessAsync();
    Task<string> GetColorAsync();
    Task SetAnimationModeIndexAsync(int index);
    Task SetBrightnessAsync(int brightness);
    Task SetColorAsync(string hexColorString);
}