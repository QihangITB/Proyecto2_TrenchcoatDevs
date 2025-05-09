using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReflexSprayPassive", menuName = "Passives/ReflexSprayPassive")]
public class ReflexSprayPassive : APassive
{
    public AAttack ReflexSpray;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(ReflexSpray))
        {
            player.knownAttacks.Add(ReflexSpray);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
