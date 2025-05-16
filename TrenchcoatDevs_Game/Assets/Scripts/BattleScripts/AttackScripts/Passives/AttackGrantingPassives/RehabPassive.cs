using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RehabPassive", menuName = "Passives/RehabPassive")]
public class RehabPassive : APassive
{
    public AAttack Rehab;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(Rehab))
        {
            player.knownAttacks.Add(Rehab);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
