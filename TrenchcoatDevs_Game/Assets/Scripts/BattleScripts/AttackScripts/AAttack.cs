using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttack : ScriptableObject
{
    public string attackName;
    public string description;
    public int cost;
    public abstract void Effect(List<CharacterHolder> targets, CharacterHolder user);
}
