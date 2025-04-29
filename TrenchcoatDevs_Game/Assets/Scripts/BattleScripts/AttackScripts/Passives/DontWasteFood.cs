using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DontWasteFood", menuName = "Passives/DontWasteFood")]
public class DontWasteFood : APassive
{
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        //la pasiva no se activa sola, sirve para comprobar si algun personaje la tiene al morir un enemigo
    }
}
