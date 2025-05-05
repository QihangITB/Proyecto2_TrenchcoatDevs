using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RockAbdomen", menuName = "Passives/RockAbdomen")]
public class RockAbdomen : APassive
{
    // Start is called before the first frame update
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        player.defense += 2;
    }
}
