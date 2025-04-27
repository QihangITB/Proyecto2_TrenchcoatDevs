using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSneakers", menuName = "Passives/NewSneakers")]

public class NewSneakers : APassive
{
    public override void ActivatePassive(CharacterHolder player)
    {
        player.speed += 4;
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
}
