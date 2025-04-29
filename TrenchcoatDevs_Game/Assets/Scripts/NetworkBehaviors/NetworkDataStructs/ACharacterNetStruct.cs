using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;

public struct ACharacterNetStruct : INetworkSerializable
{
    private int _stamina;
    private int _maxStamina;
    private FixedString512Bytes _description;
    private FixedList4096Bytes<APassiveNetStruct> _passives;
    private FixedList4096Bytes<APassiveNetStruct> _knowablePassives;
    public int Stamina
    {
        set
        {
            _stamina = value;
        }
    }
    public int MaxStamina
    {
        set
        {
            _maxStamina = value;
        }
    }
    public string Description
    {
        set
        {
            _description = value;
        }
    }
    public List<APassive> Passives
    {
        set
        {
            FixedList4096Bytes<APassiveNetStruct> passives = new FixedList4096Bytes<APassiveNetStruct>();
            
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref _stamina);
        serializer.SerializeValue(ref _stamina);
        serializer.SerializeValue(ref _stamina);
    }
}