using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.UI;

public struct ACharacterNetStruct
{
    public int health;
    public int maxHealth;
    public int damage;
    public int speed;
    public int defense;
    public List<AAttack> attacks;
    public GenericAttack basicAttack;
    public string characterName;
    public int spriteIndex;
}