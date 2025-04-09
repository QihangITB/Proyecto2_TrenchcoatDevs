using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NodeAccess : MonoBehaviour
{
    public GameObject AccesButtonMenu;
    public GameObject NodeMap;
    public List<GameObject> NodeTypes;
    public List<GameObject> NodeCanvas;

    public static event Action OnReturnToNodeMap;

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
            _nodeRelations.Add(nodeName, NodeCanvas[i].name);
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
        _accesBtn.onClick.AddListener(() => OnAccessButtonClick(sceneName));
        AccesButtonMenu.SetActive(true);
    }

    private void OnAccessButtonClick(string sceneName)
    {
        NodeMap.SetActive(false);
        AccesButtonMenu.SetActive(false);

        GameObject nodeCanva = NodeCanvas.Find(x => x.name == sceneName);
        nodeCanva.SetActive(true);

        // Elimina el listener para evitar que se acumule
        _accesBtn.onClick.RemoveAllListeners();
    }

    public void OnExitButtonClick()
    {
        NodeMap.SetActive(true);
        foreach (GameObject nodeCanva in NodeCanvas)
        {
            nodeCanva.SetActive(false);
        }

        OnReturnToNodeMap?.Invoke();
    }
}
