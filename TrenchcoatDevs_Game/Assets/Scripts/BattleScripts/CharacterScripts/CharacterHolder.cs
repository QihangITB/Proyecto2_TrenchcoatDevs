using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHolder : MonoBehaviour
{
    public ACharacter character;
    public CharacterOutOfBattle characterOutOfBattle;
    public GameObject characterTurnIndicator;
    public GameObject poisonIcon;
    public GameObject disgustIcon;
    public GameObject burnIcon;
    public GameObject regenerateIcon;
    public GameObject restIcon;
    public GameObject tauntIcon;
    public int HP;
    public int maxHP;
    public int attack;
    public int speed;
    public int defense;
    public int precisionModifier;
    public int healingModifier;
    public GameObject HpBar;
    public GameObject StaminaBar;
    public int staminaRecovery;
    public int stamina;
    public int maxStamina;
    public bool isPoisoned;
    public bool poisonInmune;
    public bool isDisgusted;
    public bool disgustInmune;
    public bool isBurnt;
    public bool burnInmune;
    public bool isRegenerating;
    public bool isRested;
    public bool isTaunting;

    GameObject hitSprite, healSprite;

    public void SelectCharacter(CharacterOutOfBattle characterOutOfBattle)
    {
        hitSprite = transform.parent.Find("Hit").gameObject;
        healSprite = transform.parent.Find("Heal").gameObject;
        //apaga los iconos de estado
        poisonIcon.SetActive(false);
        disgustIcon.SetActive(false);
        burnIcon.SetActive(false);
        regenerateIcon.SetActive(false);
        restIcon.SetActive(false);
        tauntIcon.SetActive(false);
        if (character != null)
        {
            if (characterOutOfBattle != null)
            {
                this.characterOutOfBattle = characterOutOfBattle;
                HP = characterOutOfBattle.characterHP;
            }
            else
            {
                this.characterOutOfBattle = null;
                HP = character.health;
            }
            maxHP = character.maxHealth;
            attack = character.damage;
            speed = character.speed;
            defense = character.defense;
            precisionModifier = 10;
            healingModifier = 1;
            staminaRecovery = 4;
            UpdateHPBar();
        }
        
    }
    public void TakeDamage(int damage)
    {
        hitSprite.GetComponent<Image>().enabled = true;
        hitSprite.GetComponent<AudioSource>().Play();
        StartCoroutine(UnableHit());
        if (isBurnt)
        {
            damage *= 2;
        }
        if (damage- defense <= 0)
        {
            damage = 1;
        }
        else
        {
            damage -= defense;
        }
        HP -= damage;
        BattleManager.instance.basicAttackButton.GetComponent<SelectTypeOfAttack>().description.transform.parent.gameObject.SetActive(true);
        BattleManager.instance.basicAttackButton.GetComponent<SelectTypeOfAttack>().description.text = character.characterName + " took " + damage + " damage";
        Debug.Log(gameObject + " took " + damage + " damage");
        if (HP <= 0)
        {
            HP = 0;
            poisonIcon.SetActive(false);
            disgustIcon.SetActive(false);
            burnIcon.SetActive(false);
            regenerateIcon.SetActive(false);
            restIcon.SetActive(false);
            tauntIcon.SetActive(false);
            Debug.Log(gameObject+" Is dead");
            BattleManager.instance.basicAttackButton.GetComponent<SelectTypeOfAttack>().description.text = character.characterName + " died";
            BattleManager.instance.CharOrderInTurn.Remove(this);
            BattleManager.instance.characters.Remove(this);
            if (BattleManager.instance.enemies.Contains(this))
            {
                //busca entre todas las pasivas de las lista de players si alguna tiiene dontwaste food
                foreach (CharacterHolder player in BattleManager.instance.players)
                {
                    if (player.character != null)
                    {
                        foreach (APassive passive in player.characterOutOfBattle.knownPassives)
                        {
                            if (passive is DontWasteFood)
                            {
                                foreach (CharacterHolder healedPlayer in BattleManager.instance.players)
                                {
                                    if (healedPlayer.character != null)
                                    {
                                        healedPlayer.Heal(healedPlayer.maxHP / 5, false);

                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            for (int i = 0; i < BattleManager.instance.enemyButtons.Count; i++)
            {
                if (BattleManager.instance.enemyButtons[i] == this.gameObject)
                {
                    BattleManager.instance.enemyButtons[i].GetComponent<CharacterHolder>().character=null;
                    break;
                }
            }
            BattleManager.instance.playerButtons.Remove(this.gameObject);
            foreach (Image image in GetComponentsInParent<Image>())
            {
                image.enabled = false;
            }
            HpBar.SetActive(false);
            if (StaminaBar != null)
            {
                StaminaBar.SetActive(false);
            }
            character = null;

            BattleManager.instance.CheckWin();
        }
        UpdateHPBar();
    }
    public void GetPoisoned()
    {
        if (poisonInmune)
        {
            Debug.Log(character + " is inmune to poison");
        }
        else
        {
            if (!isPoisoned)
            {
                //busca entre todas las pasivas de las lista de players si alguna tiene PoisonRush
                foreach (CharacterHolder player in BattleManager.instance.players)
                {
                    foreach (APassive passive in player.characterOutOfBattle.knownPassives)
                    {
                        if (passive is PoisonRush)
                        {
                            attack += 3;
                            speed += 3;
                        }
                    }
                }
                poisonIcon.SetActive(true);
                isPoisoned = true;
                Debug.Log(character + " is now poisoned");
            }
        }
    }
    public void GetUnPoisoned()
    {
        isPoisoned = false;
        poisonIcon.SetActive(false);
        foreach (CharacterHolder player in BattleManager.instance.players)
        {
            foreach (APassive passive in player.characterOutOfBattle.knownPassives)
            {
                if (passive is PoisonRush)
                {
                    attack -= 3;
                    speed -= 3;
                    Debug.Log(character.characterName + " is now slower and weaker");
                }
            }
        }
    }
    public void GetDisgusted()
    {
        if (disgustInmune)
        {
            Debug.Log(character + " is inmune to disgust");
        }
        else
        {
            disgustIcon.SetActive(true);
            isDisgusted = true;
            Debug.Log(character + " is now disgusted");
        }

    }
    public void GetBurnt()
    {
        if (burnInmune)
        {
            Debug.Log(character + " is inmune to burn");
        }
        else
        {
            burnIcon.SetActive(true);
            isBurnt = true;
            Debug.Log(character + " is now burnt");
        }

    }
    public void GetRegenerating()
    {
        regenerateIcon.SetActive(true);
        isRegenerating = true;
        Debug.Log(character + " is now regenerating");
    }
    public void GetRested()
    {
        restIcon.SetActive(true);
        isRested = true;
        Debug.Log(character + " is now rested");
    }
    public void Taunt()
    {
        tauntIcon.SetActive(true);
        isTaunting = true;
        Debug.Log(character + " is now taunting");
    }
    public void UpdateHPBar()
    {
        HpBar.GetComponent<Slider>().value = (HP * 100 / maxHP) / 100f;
    }

    public void UpdateStaminaBar()
    {
        StaminaBar.GetComponent<Slider>().value = (stamina * 100 / maxStamina) / 100f;
    }
    public void Heal(int healing, bool overheal)
    {
        healSprite.GetComponent<Image>().enabled = true;
        healSprite.GetComponent<AudioSource>().Play();
        StartCoroutine(UnableHeal());
        Debug.Log(character + " healed for " + healing);
        HP += healing;
        if (HP > maxHP && !overheal)
        {
            HP = maxHP;
        }
        UpdateHPBar();
    }
    public void Rest(int restValue)
    {
        stamina += restValue;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        UpdateStaminaBar();
    }
    public void UseStamina(int staminaValue)
    {
        stamina -= staminaValue;
        if (stamina < 0)
        {
            stamina = 0;
        }
        UpdateStaminaBar();
    }

    IEnumerator UnableHit()
    {
        yield return new WaitForSeconds(0.5f);

        hitSprite.GetComponent<Image>().enabled = false;
    }
    IEnumerator UnableHeal()
    {
        yield return new WaitForSeconds(0.5f);

        healSprite.GetComponent<Image>().enabled = false;
    }
}
