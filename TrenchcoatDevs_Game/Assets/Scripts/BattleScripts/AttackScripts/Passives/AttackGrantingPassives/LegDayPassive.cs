using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LegDayPassive", menuName = "Passives/LegDayPassive")]
public class LegDayPassive : APassive
{
    public AAttack LegDay;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(LegDay))
        {
            player.knownAttacks.Add(LegDay);
        }
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
