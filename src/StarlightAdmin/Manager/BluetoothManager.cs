using System.Text;
using Blazm.Bluetooth;

namespace StarlightAdmin.Manager;

public class BluetoothManager : IBluetoothManager
{
    private static readonly string _serviceId = "70d07a6c-ba09-4c2f-995c-502eb3f1dd29";
    private static readonly string _characteristicBrightnessId = "e1293490-510d-48a0-8b0f-9530d2b1d169";
    private static readonly string _characteristicColorId = "476faf19-b5f5-402c-abc5-313032ffde3b";
    private static readonly string _characteristicAnimationModeId = "c09f2e13-a6f4-44b0-a843-1891eeff8282";
    private readonly IBluetoothNavigator _bluetoothNavigator;

    private Device? _bluetoothDevice;

    public bool IsConnected => _bluetoothDevice != null;

    private bool _commandIsRunning = false;

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

    public async Task SetBrightnessAsync(int brightness)
    {
        if (_commandIsRunning) return;

        _commandIsRunning = true;
        await _bluetoothDevice!.WriteValueAsync(_serviceId, _characteristicBrightnessId, BitConverter.GetBytes(brightness));
        _commandIsRunning = false;
    }

    public async Task SetColorAsync(string hexColorString)
    {
        if (_commandIsRunning) return;

        _commandIsRunning = true;
        await _bluetoothDevice!.WriteValueAsync(_serviceId, _characteristicColorId, Encoding.Default.GetBytes(hexColorString));
        _commandIsRunning = false;
    }

    public async Task SetAnimationModeIndexAsync(int index)
    {
        if (_commandIsRunning) return;

        _commandIsRunning = true;
        await _bluetoothDevice!.WriteValueAsync(_serviceId, _characteristicAnimationModeId, BitConverter.GetBytes(index));
        _commandIsRunning = false;
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

    public async Task<string> GetColorAsync()
    {
        if (!IsConnected)
        {
            return string.Empty;
        }

        var bytes = await _bluetoothDevice!.ReadValueAsync(_serviceId, _characteristicColorId);
        return Encoding.Default.GetString(bytes);
    }
}
