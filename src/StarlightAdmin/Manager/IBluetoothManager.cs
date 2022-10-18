namespace StarlightAdmin.Manager;

public interface IBluetoothManager
{
    bool IsConnected { get; }

    Task ConnectWithDeviceAsync();
    Task<int> GetBrightnessAsync();
    Task SetBrightnessAsync(int brightness);
    Task SetColorAsync(string hexColorString);
}