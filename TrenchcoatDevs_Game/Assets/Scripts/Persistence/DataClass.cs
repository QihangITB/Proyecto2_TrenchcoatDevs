using System.Collections.Generic;

// NODE MAP

[System.Serializable]
public class NodeMapSaveData
{
    public string tutorial;
    public string start;
    public Level level1;
    public Level level2;
    public Level level3;
    public Level level4;
    public Level level5;
    public string boss;
    public PlayerPosition playerPosition;
}

[System.Serializable]
public class  Level
{
    public string node1;
    public string node2;
    public string node3;
    public string node4;
    public string node5;
}

[System.Serializable]
public class PlayerPosition
{
    public float x;
    public float y;
    public float z;
}

// CHARACTERS

[System.Serializable]
public class CharacterSaveData
{
    public List<CharacterJson> characters;
    public List<string> recruitment;
}

[System.Serializable]
public class CharacterJson
{
    public string character;
    public int characterHP;
    public int characterPoisonModifier;
    public List<string> knownPassives = new List<string>();
    public List<string> knownAttacks = new List<string>();
    public string basicAttack;
    public int level;
    public int timesToLevelUp;
    public int fightsToLevelUp;
}
