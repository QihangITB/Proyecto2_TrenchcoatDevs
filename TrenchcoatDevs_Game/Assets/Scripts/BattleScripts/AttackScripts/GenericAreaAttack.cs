using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAreaAttack : ScriptableObject
{
    public abstract void Effect(List<CharacterHolder> targets, CharacterHolder user);
}
