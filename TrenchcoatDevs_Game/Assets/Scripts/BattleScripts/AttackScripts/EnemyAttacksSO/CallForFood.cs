using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CallForFood", menuName = "EnemyAttacks/CallForFood")]
public class CallForFood : GenericAttack
{
    public ACharacter enemyToCall;
    public override void ActivateTargetButtons()
    {
    }
    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        for (int i = 0; i < BattleManager.instance.enemySelectors.Count; i++)
        {
            if (BattleManager.instance.enemySelectors[i].character == null || BattleManager.instance.enemySelectors[i].character.characterName == "Onion")
            {
                BattleManager.instance.enemySelectors[i].character = enemyToCall;
                Debug.Log("tengo " + BattleManager.instance.enemySelectors[i].character);

                /*for (int j = 0; j < BattleManager.instance.enemySelectors[i].gameObject.GetComponentsInParent<Image>().Count(); j++)
                {
                    if (j == 1)
                    {
                        //BattleManager.instance.enemySelectors[i].gameObject.GetComponentsInParent<Image>()[j].enabled = true;
                    }
                }*/
                GameObject parent = BattleManager.instance.enemySelectors[i].gameObject.transform.parent.gameObject;
                if (parent.GetComponentInChildren<RawImage>())
                {
                    GameObject sprite = parent.GetComponentInChildren<RawImage>().gameObject;
                    sprite.GetComponent<SelectSpriteInBattle>().StartCoroutine(sprite.GetComponent<SelectSpriteInBattle>().SelectSprite());
                }

                BattleManager.instance.enemySelectors[i].HpBar.GetComponent<Slider>().gameObject.SetActive(true);
                BattleManager.instance.enemySelectors[i].SelectCharacter(null);
                BattleManager.instance.enemies.Add(BattleManager.instance.enemySelectors[i]);
                BattleManager.instance.basicAttackButton.GetComponent<SelectTypeOfAttack>().description.transform.parent.gameObject.SetActive(true);
                BattleManager.instance.basicAttackButton.GetComponent<SelectTypeOfAttack>().description.text = user.character.characterName + " calls for food";
                if (BattleManager.instance.enemyButtons[1]== BattleManager.instance.enemySelectors[i].gameObject)
                {
                    BattleManager.instance.enemyButtons[3] = BattleManager.instance.enemySelectors[i].gameObject;
                }
                else
                {
                    BattleManager.instance.enemyButtons[1] = BattleManager.instance.enemySelectors[i].gameObject;
                }
            }
        }
    }
}
