using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHolder : MonoBehaviour
{
    public ACharacter character;
    public int HP;
    public int maxHP;
    public int attack;
    public int speed;
    public int defense;
    public GameObject HpBar;
    public GameObject StaminaBar;
    public int stamina;
    public int maxStamina;
    public bool isPoisoned;
    public bool isDisgusted;
    public bool isBurnt;
    public bool isRegenerating;
    public bool isRested;


    public void SelectCharacter()
    {
        HP = character.health;
        maxHP = character.maxHealth;
        attack = character.damage;
        speed = character.speed;
        defense = character.defense;
    }
    public void TakeDamage(int damage)
    {
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
        Debug.Log(gameObject + " took " + damage + " damage");
        if (HP <= 0)
        {
            HP = 0;
            Debug.Log(gameObject+" Is dead");
            BattleManager.instance.CharOrderInTurn.Remove(this);
            BattleManager.instance.characters.Remove(this);
            BattleManager.instance.players.Remove(this);
            BattleManager.instance.enemies.Remove(this);
            BattleManager.instance.CheckWin();
        }
        UpdateHPBar();
    }
    public void GetPoisoned()
    {
        isPoisoned = true;
        Debug.Log(character + " is now poisoned");
    }
    public void GetDisgusted()
    {
        isDisgusted = true;
        Debug.Log(character + " is now disgusted");
    }
    public void GetBurnt()
    {
        isBurnt = true;
        Debug.Log(character + " is now burnt");
    }
    public void GetRegenerating()
    {
        isRegenerating = true;
        Debug.Log(character + " is now regenerating");
    }
    public void GetRested()
    {
        isRested = true;
        Debug.Log(character + " is now rested");
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
}
