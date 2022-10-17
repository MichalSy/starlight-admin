using Blazm.Bluetooth;

namespace StarlightAdmin.Manager;

public class BluetoothManager : IBluetoothManager
{
    private static readonly string _serviceId = "70d07a6c-ba09-4c2f-995c-502eb3f1dd29";
    private static readonly string _characteristicBrightnessId = "e1293490-510d-48a0-8b0f-9530d2b1d169";
    private readonly IBluetoothNavigator _bluetoothNavigator;

    private Device? _bluetoothDevice;

    public bool IsConnected => _bluetoothDevice != null;

    public BluetoothManager(IBluetoothNavigator bluetoothNavigator)
    {
        _bluetoothNavigator = bluetoothNavigator ?? throw new ArgumentNullException(nameof(bluetoothNavigator));
    }

    public async Task ConnectWithDeviceAsync()
    {
        if (_bluetoothDevice != null)
        {
            return;
        }
        var q = new RequestDeviceQuery();
        q.Filters.Add(new Filter { Name = "Starlight" });
        q.OptionalServices.Add("70d07a6c-ba09-4c2f-995c-502eb3f1dd29");

        _bluetoothDevice = await _bluetoothNavigator.RequestDeviceAsync(q);
    }

    public async Task<int> GetBrightnessAsync()
    {
        if (!IsConnected)
        {
            return default;
        }

        var bytes = await _bluetoothDevice!.ReadValueAsync(_serviceId, _characteristicBrightnessId);
        return BitConverter.ToInt32(bytes);
    }

    public async Task SetBrightnessAsync(int brightness)
    {
        await _bluetoothDevice!.WriteValueAsync(_serviceId, _characteristicBrightnessId, BitConverter.GetBytes(brightness));
    }

}
