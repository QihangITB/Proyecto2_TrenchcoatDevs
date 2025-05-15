using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ProteinShake", menuName = "Attacks/ProteinShake")]
public class ProteinShake : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        foreach (GameObject button in BattleManager.instance.playerButtons)
        {
            if (button.GetComponent<CharacterHolder>().character != null)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
    }

    public override void Effect(List<CharacterHolder> target, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            BattleManager.instance.FinishTurn();
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
