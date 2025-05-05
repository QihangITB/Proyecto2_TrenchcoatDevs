using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAreaAttack : AAttack
{
    public List<GameObject> targetButtons;
    public abstract void ActivateTargetButtons();
}
