using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct APlayableCharacterNetStruct : INetworkSerializable
{
    private int _stamina;
    private int _maxStamina;
    private FixedString512Bytes _description;
    private APassiveNetStruct[] _passives;
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
            List<APassiveNetStruct> passives = new List<APassiveNetStruct>();
            foreach (APassive passive in value)
            {
                passives.Add(passive.StructForm());
            }
            _passives = passives.ToArray();
        }
        get
        {
            List<APassive> passives = new List<APassive>();
            foreach(APassiveNetStruct passive in _passives)
            {
                APassive passiveClass = ScriptableObject.CreateInstance<APassive>();
                passiveClass.SetFromStruct(passive);
                passives.Add(passiveClass);
            }
            return passives;
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref _stamina);
        serializer.SerializeValue(ref _maxStamina);
        serializer.SerializeValue(ref _description);

        int length = 0;
        if (!serializer.IsReader)
        {
            length = _passives.Length;
        }
        serializer.SerializeValue(ref length);
        if (serializer.IsReader)
        {
            _passives = new APassiveNetStruct[length];
        }
        for (int i = 0; i < length; i++)
        {
            serializer.SerializeValue(ref _passives[i]);
        }
    }
}