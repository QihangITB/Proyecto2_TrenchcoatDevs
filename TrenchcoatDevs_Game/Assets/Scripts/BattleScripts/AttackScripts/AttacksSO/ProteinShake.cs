using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ProteinShake", menuName = "Attacks/ProteinShake")]
public class ProteinShake : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        targetButtons.Clear();
        //pon el boton de user de battlemanager como boton en buttontargetlist
        targetButtons = new List<GameObject>
        {
            BattleManager.instance.user.gameObject
        };
        foreach (GameObject button in targetButtons)
        {
            button.GetComponent<Image>().enabled = true;
        }
    }

    public override void Effect(List<CharacterHolder> target, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder character in target)
            {
                character.attack += 1;
                character.stamina += character.maxStamina/2;
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
