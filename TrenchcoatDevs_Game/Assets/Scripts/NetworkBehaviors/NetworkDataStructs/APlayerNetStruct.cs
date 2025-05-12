using System;
using System.Collections.Generic;
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
    public int basicAttackIndex;
    public int[] passivesindexes;
    public int[] attacksIndex;
    
    public APlayerNetStruct(APlayer playerClass, FightAssetsIndexer assetIndexer)
    {
        health = playerClass.health;
        maxHealth = playerClass.maxHealth;
        damage = playerClass.damage;
        speed = playerClass.speed;
        defense = playerClass.defense;
        characterName = playerClass.characterName;
        if(playerClass.sprite == null)
        {
            spriteIndex = 0;
        }
        else
        {
            spriteIndex = assetIndexer.GetSpriteIndex(playerClass.sprite);
        }
        
        stamina = playerClass.stamina;
        maxStamina = playerClass.maxStamina;
        description = playerClass.description;
        passivesindexes = new int[playerClass.passives.Count];
        basicAttackIndex = assetIndexer.GetAttackIndex(playerClass.basicAttack);
        for(int i = 0; i<passivesindexes.Length; i++)
        {
            passivesindexes[i] = assetIndexer.GetPassiveIndex(playerClass.passives[i]);
        }
        attacksIndex = new int[playerClass.attacks.Count];
        for(int i = 0; i<attacksIndex.Length; i++)
        {
            attacksIndex[i] = assetIndexer.GetAttackIndex(playerClass.attacks[i]);
        }
    }
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref health);
        serializer.SerializeValue(ref maxHealth);
        serializer.SerializeValue(ref damage);
        serializer.SerializeValue(ref speed);
        serializer.SerializeValue(ref defense);
        serializer.SerializeValue(ref characterName);
        serializer.SerializeValue(ref spriteIndex);
        serializer.SerializeValue(ref basicAttackIndex);

        int lengthPassives = 0;
        int lengthAttacks = 0;
        if (!serializer.IsReader)
        {
            lengthPassives = passivesindexes == null ? 0 : passivesindexes.Length;
            lengthAttacks = attacksIndex == null ? 0 : attacksIndex.Length;
        }
        serializer.SerializeValue(ref lengthPassives);
        serializer.SerializeValue(ref lengthAttacks);
        if (serializer.IsReader)
        {
            passivesindexes = new int[lengthPassives];
            attacksIndex = new int[lengthPassives];
        }
        for (int i = 0; i < lengthPassives; i++)
        {
            serializer.SerializeValue(ref passivesindexes[i]);
        }
        for(int i = 0; i < lengthPassives; i++)
        {
            serializer.SerializeValue(ref attacksIndex[i]);
        }
  
    }
}
