using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTypeOfAttack : MonoBehaviour
{
    public SelectAttack attack;
    public SelectAreaAttack areaAttack;
    public bool isAreaAttack;

    public void SelectAttack()
    {
        if (isAreaAttack)
        {
            areaAttack.SelectGenericAreaAttack();
        }
        else
        {
            attack.SelectGenericAttack();
        }
    }
}
