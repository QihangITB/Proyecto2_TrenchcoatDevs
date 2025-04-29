using Unity.Collections;
using Unity.Netcode;

public struct APassiveNetStruct : INetworkSerializable
{
    public FixedString64Bytes _passiveName;
    public FixedString128Bytes _passiveDescription;

    public string PassiveName
    {
        set
        {
            _passiveName = value;
        }
        get
        {
            return _passiveName.ToString();
        }
    }
    public string PassiveDescription
    {
        set
        {
            _passiveDescription = value;
        }
        get
        {
            return _passiveDescription.ToString();
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref _passiveName);
        serializer.SerializeValue(ref _passiveDescription);
    }
}