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
    public NodeMapGeneration nodeMapGeneration;
    public EnemySelectionPool enemySelectionPool;
    public List<Camera> cams;

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
        if (sceneName == "Battle")
        {
            enemySelectionPool.AllocateEnemies(nodeMapGeneration.GetPlayerLevel());
            cams[0].gameObject.SetActive(false);
            cams[1].gameObject.SetActive(true);
            Debug.Log("a");
        }
        // Elimina el listener para evitar que se acumule
        _accesBtn.onClick.RemoveAllListeners();
    }

    public void OnExitButtonClick()
    {
        bool hasToLevelUp = false;
        foreach (GameObject nodeCanva in NodeCanvas)
        {
            nodeCanva.SetActive(false);
        }
        foreach (CharacterHolder character in BattleManager.instance.characters)
        {
            if (character.characterOutOfBattle != null)
            {
                
                if (character.characterOutOfBattle.timesToLevelUp > 0)
                {
                    hasToLevelUp = true;
                    character.characterOutOfBattle.LevelUp();
                    //Busca en nodeCanva el canvas de subir de nivel 
                    GameObject nodeCanva = NodeCanvas.Find(x => x.name == "LevelUp");
                    nodeCanva.SetActive(true);
                    SkillSelection.Instance.characterOutOfBattle = character.characterOutOfBattle;
                    SkillSelection.Instance.showAbilities();
                }
                character.characterOutOfBattle = null;
                character.character = null;
            }
        }
        if (!hasToLevelUp)
        {
            cams[0].gameObject.SetActive(true);
            cams[1].gameObject.SetActive(false);
            NodeMap.SetActive(true);
            OnReturnToNodeMap?.Invoke();
        }
    }
}
