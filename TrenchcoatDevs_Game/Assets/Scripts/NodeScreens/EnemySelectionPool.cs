using System.Collections;
using System.Collections.Generic;
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
        switch (level)
        {
            case 0:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(onion);
                break;
            case 1:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(onion);
                PlayerManager.instance.enemies.Add(onion);
                break;
            case 2:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(onion);
                PlayerManager.instance.enemies.Add(onion);
                break;
            case 3:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(onion);
                PlayerManager.instance.enemies.Add(onion);
                PlayerManager.instance.enemies.Add(onion);
                break;
            case 4:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(onion);
                PlayerManager.instance.enemies.Add(broColi);
                PlayerManager.instance.enemies.Add(broColi);
                break;
            case 5:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(PGeon);
                PlayerManager.instance.enemies.Add(onion);
                PlayerManager.instance.enemies.Add(onion);
                break;
            case 6:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(broColi);
                PlayerManager.instance.enemies.Add(broColi);
                PlayerManager.instance.enemies.Add(broColi);
                break;
            case 7:
                PlayerManager.instance.enemies.Clear();
                PlayerManager.instance.enemies.Add(princess);
                break;
            default:
                Debug.Log("No enemies available for this level.");
                break;
        }
        PlayerManager.instance.AllocateCharacters();

    }
}
