using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ThickClothes", menuName = "Passives/ThickClothes")]

public class ThickClothes : APassive
{
    public override void ActivatePassive(CharacterHolder player)
    {
        player.burnInmune = true;
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
}
