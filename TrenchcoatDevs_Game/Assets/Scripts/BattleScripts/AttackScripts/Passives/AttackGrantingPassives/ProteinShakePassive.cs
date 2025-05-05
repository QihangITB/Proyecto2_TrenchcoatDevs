using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProteinShakePassive", menuName = "Passives/ProteinShakePassive")]
public class ProteinShakePassive : APassive
{
    public AAttack ProteinShake;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(ProteinShake))
        {
            player.knownAttacks.Add(ProteinShake);
        }
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
