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
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
