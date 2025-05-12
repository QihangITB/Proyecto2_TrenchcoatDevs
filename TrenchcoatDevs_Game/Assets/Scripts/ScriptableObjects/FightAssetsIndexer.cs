using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "FightAssetsIndexer", menuName = "FightAssetsIndexer")]
/**
 *<summary>
 *  This scriptable object has been created to index any prefab or asset that may appear in a fight
 *  to easily access it when the network says tha X character is on X spot when starting the network fight.
 *</summary>
 **/
public class FightAssetsIndexer : ScriptableObject
{
    [SerializeField]
    List<Sprite> _sprites;
    [SerializeField]
    List<AAttack> _attacks;
    [SerializeField]
    List<APassive> _passives;

    public int GetSpriteIndex(Sprite sprite)
    {
        return _sprites.IndexOf(sprite);
    }
    public Sprite GetSprite(int index)
    {
        return _sprites[index];
    }
    public int GetAttackIndex(AAttack attack)
    {
        return _attacks.IndexOf(attack);
    }
    public AAttack GetAttack(int index)
    {
        return _attacks[index];
    }
    public APassive GetPassive(int index)
    {
        return _passives[index];
    }
    public int GetPassiveIndex(APassive passive)
    {
        return _passives.IndexOf(passive);
    }
}
