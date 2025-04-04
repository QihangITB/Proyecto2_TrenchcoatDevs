using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rest", menuName = "Attacks/Rest")]
public class Rest : BasicAttackScript
{
    public int staminaRestored = 4;
    public override void ActivateTargetButtons()
    {
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

    public override void Effect(List<CharacterHolder> target, CharacterHolder user)
    {
        //recupera stamina
        user.stamina += staminaRestored;
        if (user.stamina > user.maxStamina)
        {
            user.stamina = user.maxStamina;
        }
        user.UpdateStaminaBar();
        BattleManager.instance.FinishTurn();
    }
}