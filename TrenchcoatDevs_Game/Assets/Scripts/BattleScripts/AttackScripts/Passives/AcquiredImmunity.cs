using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AcquiredImmunity", menuName = "Passives/AcquiredImmunity")]
public class AcquiredImmunity : APassive
{
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        player.characterOutOfBattle.characterPoisonModifier = 0;
    }
}
