using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySelectionPool : MonoBehaviour
{
    public AEnemy onion;
    public AEnemy broColi;
    public AEnemy PGeon;
    public AEnemy princess;

    public void AllocateEnemies(int level)
    {
        //haz un switch case donde level pueda ir de 0 a 7  2/5 reclutar
        //en cada caso asigna un enemigo diferente a la variable enemy
        PlayerManager.instance.enemies.Clear();
        PlayerManager.instance.enemies.Add(null);
        PlayerManager.instance.enemies.Add(null);
        PlayerManager.instance.enemies.Add(null);
        switch (level)
        {
            case 0:
                
                PlayerManager.instance.enemies[0] = Instantiate(onion);
                break;
            case 1:
                PlayerManager.instance.enemies[0] = Instantiate(onion);
                PlayerManager.instance.enemies[1] = Instantiate(onion);
                break;
            case 2:
                PlayerManager.instance.enemies[0] = Instantiate(onion);
                PlayerManager.instance.enemies[1] = Instantiate(onion);
                break;
            case 3:
                PlayerManager.instance.enemies[0] = Instantiate(onion);
                PlayerManager.instance.enemies[1] = Instantiate(onion);
                PlayerManager.instance.enemies[2] = Instantiate(onion);
                break;
            case 4:
                PlayerManager.instance.enemies[0] = Instantiate(onion);
                PlayerManager.instance.enemies[1] = Instantiate(broColi);
                PlayerManager.instance.enemies[2] = Instantiate(broColi);
                break;
            case 5:
                PlayerManager.instance.enemies[0] = Instantiate(PGeon);
                PlayerManager.instance.enemies[1] = Instantiate(onion);
                PlayerManager.instance.enemies[2] = Instantiate(onion);
                break;
            case 6:
                PlayerManager.instance.enemies[0] = Instantiate(broColi);
                PlayerManager.instance.enemies[1] = Instantiate(broColi);
                PlayerManager.instance.enemies[2] = Instantiate(broColi);
                break;
            case 7:
                PlayerManager.instance.enemies[0] = Instantiate(princess);
                break;
            default:
                Debug.Log("No enemies available for this level.");
                break;
        }
        PlayerManager.instance.AllocateCharacters();

    }
}
