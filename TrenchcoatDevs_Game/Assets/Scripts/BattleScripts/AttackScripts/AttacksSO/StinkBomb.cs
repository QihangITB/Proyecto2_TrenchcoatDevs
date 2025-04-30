using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StinkBomb", menuName = "Attacks/StinkBomb")]
public class StinkBomb : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        targetButtons.Clear();
        targetButtons = BattleManager.instance.enemyButtons;
        foreach (GameObject button in targetButtons)
        {
            if (button.GetComponent<CharacterHolder>().character != null)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                target.GetDisgusted();
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
