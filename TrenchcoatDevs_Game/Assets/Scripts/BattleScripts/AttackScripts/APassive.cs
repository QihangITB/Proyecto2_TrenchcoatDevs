using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APassive : ScriptableObject
{
    public string passiveName;
    public string passiveDescription;
    public abstract void ActivatePassive(CharacterHolder player);
    public abstract void ObtainPassive(CharacterOutOfBattle player);
}