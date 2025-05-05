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
    List<Image> _sprites;
    [SerializeField]
    List<AAttack> _attacks;

    public int GetSpriteIndex(Image sprite)
    {
        return _sprites.IndexOf(sprite);
    }
    public Image GetSprite(int index)
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
}
