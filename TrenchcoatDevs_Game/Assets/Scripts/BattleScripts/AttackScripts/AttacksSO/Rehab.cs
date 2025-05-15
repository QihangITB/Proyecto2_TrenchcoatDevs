using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Rehab", menuName = "Attacks/Rehab")]
public class Rehab : GenericAttack
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

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            BattleManager.instance.FinishTurn();
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                target.GetUnPoisoned();
                Debug.Log(target + " is cured from poison");
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
