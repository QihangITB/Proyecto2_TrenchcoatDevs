using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rest", menuName = "Attacks/Rest")]
public class Rest : BasicAttackScript
{
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle())
        {
            targetButtons.Clear();
            //pon el boton de user de battlemanager como boton en buttontargetlist
            targetButtons = new List<GameObject>
            {
                OnlineBattleManager.instance.user.gameObject
            };
            foreach (GameObject button in targetButtons)
            {
                button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
        }
        else
        {
            targetButtons.Clear();
            //pon el boton de user de battlemanager como boton en buttontargetlist
            targetButtons = new List<GameObject>
            {
                BattleManager.instance.user.gameObject
            };
            foreach (GameObject button in targetButtons)
            {
                button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
        }
        
    }

    public override void Effect(List<CharacterHolder> target, CharacterHolder user)
    {
        //recupera stamina
        user.Rest(user.staminaRecovery);
        PerformFinishTurn();
    }
}