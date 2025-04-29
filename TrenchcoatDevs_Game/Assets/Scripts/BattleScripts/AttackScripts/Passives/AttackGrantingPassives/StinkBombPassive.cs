using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StinkBombPassive", menuName = "Passives/StinkBombPassive")]
public class StinkBombPassive : APassive
{
    public AAttack StinkBomb;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(StinkBomb))
        {
            player.knownAttacks.Add(StinkBomb);
        }
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
