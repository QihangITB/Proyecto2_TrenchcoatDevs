using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NodeAccess : MonoBehaviour
{
    public GameObject AccesButtonMenu;
    public List<GameObject> NodeTypes;
    public List<string> SceneNames;

    private TMP_Text _menuTitle;
    private Button _accesBtn;
    private Dictionary<string, string> _nodeRelations;

    private void Start()
    {
        _menuTitle = AccesButtonMenu.GetComponentInChildren<TMP_Text>();
        _accesBtn = AccesButtonMenu.GetComponentInChildren<Button>();
        _nodeRelations = new Dictionary<string, string>();

        for (int i = 0; i < NodeTypes.Count; i++)
        {
            string nodeName = NodeTypes[i].name;
            _nodeRelations.Add(nodeName, SceneNames[i]);
        }
    
        NodeInteraction.OnPlayerArrivesToNode += ShowAccesButton;
    }

    private void OnDestroy()
    {
        NodeInteraction.OnPlayerArrivesToNode -= ShowAccesButton;
    }

    private void ShowAccesButton(GameObject node)
    {
        string sceneName = _nodeRelations[node.name.Replace("(Clone)", "")];
        _menuTitle.text = sceneName;
        _accesBtn.onClick.AddListener(() => OnButtonClick(sceneName));
        AccesButtonMenu.SetActive(true);
    }

    void OnButtonClick(string sceneName)
    {
        SceneController.Instance.LoadScene(sceneName);
    }
}
