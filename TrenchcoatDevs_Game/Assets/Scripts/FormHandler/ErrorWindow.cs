using System;
using TMPro;
using UnityEngine;

public class ErrorWindow : FormHandler
{
    [SerializeField]
    TMP_Text _errorText;
    public event Action OnDoAction;
    public void ChangeData(string newErrorText)
    {
        _errorText.text = newErrorText;
    }
    public override void ChangeData()
    {
        
    }

    public override void DoAction()
    {
        OnDoAction?.Invoke();
    }
}
