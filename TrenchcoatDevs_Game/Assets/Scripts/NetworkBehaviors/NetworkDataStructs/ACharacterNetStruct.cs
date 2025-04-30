using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.UI;

public struct ACharacterNetStruct : INetworkSerializable
{
    public int health;
    public int maxHealth;
    public int damage;
    public int speed;
    public int defense;
    public List<AAttack> attacks;
    public GenericAttack basicAttack;
    public string characterName;
    public Image sprite;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        throw new System.NotImplementedException();
    }
}