using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;

public struct APlayerNetStruct : INetworkSerializable
{
    public int level;
    public bool initialized;
    public bool isNull;
    public FixedString64Bytes typeName;
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
    
    public APlayerNetStruct(CharacterOutOfBattle playerClass, FightAssetsIndexer assetIndexer)
    {
        initialized = true;
        isNull = playerClass == null;
        if (!isNull)
        {
            level = playerClass.level;
            typeName = playerClass.character.GetType().Name;
            health = playerClass.character.health;
            maxHealth = playerClass.character.maxHealth;
            damage = playerClass.character.damage;
            speed = playerClass.character.speed;
            defense = playerClass.character.defense;
            characterName = playerClass.character.characterName;
            if (playerClass.character.sprite == null)
            {
                spriteIndex = 0;
            }
            else
            {
                spriteIndex = assetIndexer.GetSpriteIndex(playerClass.character.sprite);
            }

            stamina = playerClass.character.stamina;
            maxStamina = playerClass.character.maxStamina;
            description = playerClass.character.description;
            passivesindexes = new int[playerClass.character.passives.Count];
            basicAttackIndex = assetIndexer.GetAttackIndex(playerClass.basicAttack);
            for (int i = 0; i < passivesindexes.Length; i++)
            {
                passivesindexes[i] = assetIndexer.GetPassiveIndex(playerClass.character.passives[i]);
            }
            attacksIndex = new int[playerClass.character.attacks.Count];
            for (int i = 0; i < attacksIndex.Length; i++)
            {
                attacksIndex[i] = assetIndexer.GetAttackIndex(playerClass.character.attacks[i]);
            }
        }
        
        else
        {
            level = default;
            typeName = default;
            health = default;
            maxHealth = default;
            damage = default;
            speed = default;
            defense = default;
            characterName = default;
            spriteIndex = default;
            stamina = default;
            maxStamina = default;
            description = default;
            passivesindexes = new int[0];
            basicAttackIndex = default;
            attacksIndex = new int[0];
        }
        
    }
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref level);
        serializer.SerializeValue(ref initialized);
        serializer.SerializeValue(ref isNull);
        serializer.SerializeValue(ref typeName);
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
