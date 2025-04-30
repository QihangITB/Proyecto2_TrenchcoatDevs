using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ChickenBrew", menuName = "Attacks/ChickenBrew")]
public class ChickenBrew : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        targetButtons.Clear();
        targetButtons = BattleManager.instance.playerButtons;
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
                target.isBurnt = false;
                target.burnIcon.SetActive(false);
                target.isDisgusted = false;
                target.disgustIcon.SetActive(false);
                target.GetUnPoisoned();
                Debug.Log(target + " is cured from all statuses");
                target.GetRested();
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
