using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FaceMask", menuName = "Passives/FaceMask")]
public class FaceMask : APassive
{
    public override void ActivatePassive(CharacterHolder player)
    {
        player.poisonInmune = true;
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
}
