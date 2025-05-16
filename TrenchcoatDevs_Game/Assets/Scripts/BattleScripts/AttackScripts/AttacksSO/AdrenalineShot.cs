using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AdrenalineShot", menuName = "Attacks/AdrenalineShot")]
public class AdrenalineShot : GenericAttack
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
                target.attack += 2;
                target.speed += 3;
                target.defense += 1;
                target.stamina = target.maxStamina;
                target.UpdateStaminaBar();
                target.TakeDamage(target.HP / 2);

            }
            PerformFinishTurn();
        }

    }
}
