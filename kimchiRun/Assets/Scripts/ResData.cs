


[System.Serializable]
public enum tags 
{
    Enemy,
    Bullet,
    PlayerBullet,
    Item,
    EnemyBullet,
    Boss,
    player,
}

public static class TagManger
{
    public static string GetTag(tags _t)
    { 
        return _t.ToString();   
    }
}

[System.Serializable]
public enum ItemType
{ 
    Item1,
    Item2,
    Item3,
    GoldenItem
}

[System.Serializable]
public enum ItemNormal
{ 
    Item1,
    Item2,
    Item3,
}