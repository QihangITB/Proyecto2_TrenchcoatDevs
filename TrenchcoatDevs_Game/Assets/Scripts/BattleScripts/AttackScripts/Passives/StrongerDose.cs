using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StrongerDose", menuName = "Passives/StrongerDose")]
public class StrongerDose : APassive
{
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        BattleManager.instance.poisonDamageDivisor = 4;
    }
}
