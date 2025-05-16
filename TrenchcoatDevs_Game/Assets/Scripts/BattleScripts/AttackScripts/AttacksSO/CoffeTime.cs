using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CoffeTime", menuName = "Attacks/CoffeTime")]
public class CoffeTime : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle())
        {
            foreach (GameObject button in OnlineBattleManager.instance.playerButtons)
            {
                if (button.GetComponent<CharacterHolder>().character != null)
                {
                    button.GetComponent<Image>().enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject button in BattleManager.instance.playerButtons)
            {
                if (button.GetComponent<CharacterHolder>().character != null)
                {
                    button.GetComponent<Image>().enabled = true;
                }
            }
        }
        
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            PerformFinishTurn();
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                target.speed += 2;
                Debug.Log(target + " speed increased");
                target.GetRested();
            }
            PerformFinishTurn();
        }

    }
}
