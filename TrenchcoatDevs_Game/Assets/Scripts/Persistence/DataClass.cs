[System.Serializable]
public class LevelData
{
    public string tutorial;
    public string start;
    public Level level1;
    public Level level2;
    public Level level3;
    public Level level4;
    public Level level5;
    public string boss;
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