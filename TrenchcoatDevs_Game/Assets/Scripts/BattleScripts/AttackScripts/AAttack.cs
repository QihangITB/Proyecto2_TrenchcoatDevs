using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttack : ScriptableObject
{
    public abstract void Effect(List<CharacterHolder> targets, CharacterHolder user);
}
