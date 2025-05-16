using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class APlayer : ACharacter
{
    public int stamina;
    public int maxStamina;
    public string description;
    public List<APassive> passives;
    public List<APassive> knowablePassives;

    
}
