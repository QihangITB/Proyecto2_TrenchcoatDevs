using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameForm : FormHandler
{
    const string CanNotBeEmpty = "The player name can not be empty.";
    [SerializeField]
    TMP_InputField _playerNameField;
    [SerializeField]
    TMP_Text _errorField;
    public override void ChangeData()
    {
        if (VerifyData())
        {
            HostClientDiscovery.ChangePlayerName(_playerNameField.text);
        }
        
    }
    protected override bool VerifyData()
    {
        bool result = !string.IsNullOrEmpty(_playerNameField.text);
        if (!result)
        {
            _errorField.text = CanNotBeEmpty;
        }
        else
        {
            _errorField.text = string.Empty;
        }
        
        return result;
    }
    public override void DoAction()
    {
        ChangeData();
    }

    private void Awake()
    {
        _playerNameField.text = HostClientDiscovery.PlayerName;
    }
}
