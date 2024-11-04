using UnityEngine;
using MessagePack;
using System.Collections.Generic;

[MessagePackObject]
public class PlayerSaveData
{
    [Key(0)]
    public Vector3 position;
    [Key(1)]
    public Quaternion rotation;
    [Key(2)]
    public float health;
    [Key(3)]
    public List<string> weaponIds;
    [Key(4)]
    public int currentEquipedWeaponIndex;

    [Key(5)]
    public List<string> abilities;
    [Key(6)]
    public List<float> abilityCooldownTimeRemamings;
}

[MessagePackObject]
public class EnemySaveData
{
    [Key(0)]
    public string prefabId;
    [Key(1)]
    public Vector3 position;
    [Key(2)]
    public Quaternion rotation;
    [Key(3)]
    public float health;
}

[MessagePackObject]
public class GameSaveData
{
    [Key(0)]
    public int sceneBuildIndex = -1;
    
    [Key(1)]
    public PlayerSaveData playerSaveData;

    [Key(2)]
    public List<EnemySaveData> enemySaveDatas = new List<EnemySaveData>();
}
