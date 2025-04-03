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
    public GameObject HpBar;
    public GameObject StaminaBar;
    public int stamina;
    public int maxStamina;


    public void SelectEnemy()
    {
        HP = character.health;
        maxHP = character.maxHealth;
        attack = character.damage;
        speed = character.speed;

    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log("Character took " + damage + " damage");
        if (HP <= 0)
        {
            HP = 0;
            Debug.Log("Character is dead");
        }
        UpdateHPBar();
    }
    public void UpdateHPBar()
    {
        HpBar.GetComponent<Slider>().value = (HP * 100 / maxHP) / 100f;
    }

    public void UpdateStaminaBar()
    {
        StaminaBar.GetComponent<Slider>().value = (stamina * 100 / maxStamina) / 100;
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
}
