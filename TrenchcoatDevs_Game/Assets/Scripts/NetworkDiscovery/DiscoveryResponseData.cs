using Unity.Netcode;

public struct DiscoveryResponseData: INetworkSerializable
{
    public ushort Port;

    public string ServerName;

    public string playerName;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Port);
        serializer.SerializeValue(ref ServerName);
        serializer.SerializeValue(ref playerName);
    }
}
