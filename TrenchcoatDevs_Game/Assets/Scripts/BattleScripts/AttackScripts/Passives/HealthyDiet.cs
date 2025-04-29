using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthyDiet", menuName = "Passives/HealthyDiet")]
public class HealthyDiet : APassive
{
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        player.characterOutOfBattle.characterPoisonModifier = -1;
    }
}
