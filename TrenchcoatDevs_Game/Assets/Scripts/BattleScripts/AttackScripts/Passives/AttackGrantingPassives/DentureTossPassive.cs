using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DentureTossPassive", menuName = "Passives/DentureTossPassive")]
public class DentureTossPassive : APassive
{
    public AAttack DentureToss;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(DentureToss))
        {
            player.knownAttacks.Add(DentureToss);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
