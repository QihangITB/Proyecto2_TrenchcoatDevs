using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

[CreateAssetMenu(fileName = "PepperSprayPassive", menuName = "Passives/PepperSprayPassive")]
public class PepperSprayPassive : APassive
{
    public AAttack PepperSpray;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(PepperSpray))
        {
            player.knownAttacks.Add(PepperSpray);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
