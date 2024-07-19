namespace Configastic.SharedModels.Models.BolidDevices
{
    public enum OrionCommands : byte
    {
        IsOnline = 0x01,
        GetModel = 0x0D,
        ChangeAddress = 0x0F,
        WriteToDeviceMemoryMap = 0x41,
        Reboot = 0x17,
    }
}
