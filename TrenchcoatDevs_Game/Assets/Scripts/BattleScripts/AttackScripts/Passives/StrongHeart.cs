using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StrongHeart", menuName = "Passives/StrongHeart")]
public class StrongHeart : APassive
{
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        player.maxStamina += 2;
        player.stamina += 2;
    }
}
