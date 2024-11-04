using UnityEngine;
using MessagePack;

[MessagePackObject]
public class PlayerSaveData
{
    [Key(0)]
    public Vector3 position;
    [Key(1)]
    public Quaternion rotation;
    [Key(2)]
    public float health;
}

[MessagePackObject]
public class GameSaveData
{
    [Key(0)]
    public PlayerSaveData playerSaveData;
}
