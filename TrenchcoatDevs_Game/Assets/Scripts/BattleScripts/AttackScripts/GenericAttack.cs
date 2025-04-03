using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAttack : ScriptableObject
{
    public List<GameObject> targetButtons;
    public abstract void Effect(CharacterHolder target, CharacterHolder user);
    public abstract void ActivateTargetButtons();
}
