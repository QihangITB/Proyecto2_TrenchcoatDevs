using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ACharacter : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int damage;
    public int speed;
    public int defense;
    public List<AAttack> attacks;
    public GenericAttack basicAttack;
    public string characterName;
    public Sprite sprite;
}
