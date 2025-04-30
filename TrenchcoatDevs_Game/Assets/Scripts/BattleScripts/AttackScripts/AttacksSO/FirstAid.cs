using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FirstAid", menuName = "Attacks/FirstAid")]
public class FirstAid : GenericAttack
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
                target.Heal((target.maxHP / 2) * user.healingModifier, true);
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
