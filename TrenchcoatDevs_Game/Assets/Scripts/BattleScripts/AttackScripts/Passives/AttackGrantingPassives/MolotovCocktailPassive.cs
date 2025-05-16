using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MolotovCocktailPassive", menuName = "Passives/MolotovCocktailPassive")]
public class MolotovCocktailPassive : APassive
{
    public AAttack MolotovCocktail;
    public override void ActivatePassive(CharacterHolder player)
    {
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {

        if (!player.knownAttacks.Contains(MolotovCocktail))
        {
            player.knownAttacks.Add(MolotovCocktail);
        }
        player.knownPassives.Add(this);
    }
}
