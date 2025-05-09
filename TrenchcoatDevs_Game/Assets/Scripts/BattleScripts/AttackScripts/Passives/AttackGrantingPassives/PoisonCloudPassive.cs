using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonCloudPassive", menuName = "Passives/PoisonCloudPassive")]
public class PoisonCloudPassive : APassive
{
    public AAttack PoisonCloud;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(PoisonCloud))
        {
            player.knownAttacks.Add(PoisonCloud);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
