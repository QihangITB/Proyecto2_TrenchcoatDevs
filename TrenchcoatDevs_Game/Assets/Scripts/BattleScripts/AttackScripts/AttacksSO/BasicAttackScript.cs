using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Attacks/BasicAttack")]
public class BasicAttackScript : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        targetButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyButton"));
        foreach (GameObject button in targetButtons)
        {
            button.GetComponent<Image>().enabled = true;
        }
    }

    public override void Effect(CharacterHolder target, CharacterHolder user)
    {
        target.TakeDamage(user.attack);
    }
}