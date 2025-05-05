using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HaveACookie", menuName = "Attacks/HaveACookie")]
public class HaveACookie : GenericAttack
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
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                target.Heal((target.maxHP / 4)*user.healingModifier, true);
                target.GetRegenerating();
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
