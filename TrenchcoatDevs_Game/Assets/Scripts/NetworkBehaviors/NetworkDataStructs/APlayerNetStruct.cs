using Unity.Collections;
using Unity.Netcode;

public struct APlayerNetStruct : INetworkSerializable
{
    public int health;
    public int maxHealth;
    public int damage;
    public int speed;
    public int defense;
    public FixedString64Bytes characterName;
    public int spriteIndex;
    public int stamina;
    public int maxStamina;
    public string description;
    public int[] passivesindexes;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref health);
        serializer.SerializeValue(ref maxHealth);
        serializer.SerializeValue(ref damage);
        serializer.SerializeValue(ref speed);
        serializer.SerializeValue(ref defense);
        serializer.SerializeValue(ref characterName);
        serializer.SerializeValue(ref spriteIndex);

        int length = 0;
        if (!serializer.IsReader)
        {
            length = passivesindexes.Length;
        }
        serializer.SerializeValue(ref length);
        if (serializer.IsReader)
        {
            passivesindexes = new int[length];
        }
        for (int i = 0; i < length; i++)
        {
            serializer.SerializeValue(ref passivesindexes[i]);
        }
    }
}
