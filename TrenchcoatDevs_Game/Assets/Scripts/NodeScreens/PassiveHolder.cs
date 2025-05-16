using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHolder : MonoBehaviour
{
    public APassive passive;

    public void SetPassive()
    {
        passive.ObtainPassive(SkillSelection.Instance.characterOutOfBattle);
        SkillSelection.Instance.nodeAccess.OnExitButtonClick();
    }
}
