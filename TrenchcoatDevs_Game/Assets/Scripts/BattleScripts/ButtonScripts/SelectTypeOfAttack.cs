using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTypeOfAttack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SelectAttack attack;
    public SelectAreaAttack areaAttack;
    public bool isAreaAttack;
    public TextMeshProUGUI description;

    public void SelectAttack()
    {
        BattleManager.instance.DeActivateTargetButtons();
        if (isAreaAttack)
        {
            areaAttack.SelectGenericAreaAttack();
        }
        else
        {
            attack.SelectGenericAttack();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //activa el padre del descripcion
        description.transform.parent.gameObject.SetActive(true);
        if (isAreaAttack)
        {
            description.text = areaAttack.attack.attackName + "\n"+ areaAttack.attack.description;
        }
        else
        {
            description.text = attack.attack.attackName + "\n"+attack.attack.description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.transform.parent.gameObject.SetActive(false);

    }
}
