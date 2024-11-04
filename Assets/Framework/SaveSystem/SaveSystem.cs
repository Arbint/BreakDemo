using UnityEngine;
using MessagePack;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    List<ScriptableObject> saveableScriptableObjects = new List<ScriptableObject>();

    [SerializeField]
    List<GameObject> saveablePrefabs = new List<GameObject>();

    private void Awake()
    {
        _saveablePrefabMap.Clear(); 
        foreach(GameObject saveablePrefab in saveablePrefabs)
        {
            ISaveable saveable = saveablePrefab.GetComponent<ISaveable>();   
            _saveablePrefabMap.Add(saveable.GetSaveableId(), saveablePrefab);
        }

        _saveableScriptObjectMap.Clear();
        foreach(ScriptableObject saveableScriptable in saveableScriptableObjects)
        {
            ISaveable saveable = saveableScriptable as ISaveable;
            _saveableScriptObjectMap.Add(saveable.GetSaveableId(), saveableScriptable);
        }
    }

    private void Start()
    {
        TryLoadFromCachedSave();
    }

    public static void SaveGame()
    {
        GameSaveData gameSaveData = new GameSaveData();

        gameSaveData.sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        foreach(SaveComponent saveComponet in GameObject.FindObjectsByType<SaveComponent>(FindObjectsSortMode.None))
        {
            saveComponet.InsertSaveData(gameSaveData);
        }

        string savePath = GetSavePath();
        if(File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        Debug.Log($"trying to save at: {savePath}");
        File.WriteAllBytes(savePath, MessagePackSerializer.Serialize(gameSaveData));
    }

    internal static void LoadGame()
    {
        if (!File.Exists(GetSavePath()))
            return;

        GameSaveData savedData = MessagePackSerializer.Deserialize<GameSaveData>(File.ReadAllBytes(GetSavePath()));
        if(savedData != null)
        {
            _cachedSavedData = savedData;
            SceneManager.LoadScene(savedData.sceneBuildIndex);
        }
    }

    static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "save.sav");
    }

    private static void TryLoadFromCachedSave()
    {
        if (_cachedSavedData == null)
            return;

        PlayerSaveComponent playerSaveComponent = FindFirstObjectByType<PlayerSaveComponent>();
        if (playerSaveComponent)
        {
            playerSaveComponent.LoadFromPlayerSaveData(_cachedSavedData.playerSaveData);
        }

        foreach(EnemySaveComponent enemySaveComponent in FindObjectsByType<EnemySaveComponent>(FindObjectsSortMode.None))
        {
            Destroy(enemySaveComponent.gameObject);
        }

        foreach(EnemySaveData enemySaveData in _cachedSavedData.enemySaveDatas)
        {
            GameObject enemyPrefab = GetPrefabFromId(enemySaveData.prefabId);
            GameObject newEnemy = Instantiate(enemyPrefab);
            EnemySaveComponent enemySaveComponent = newEnemy.GetComponent<EnemySaveComponent>();
            enemySaveComponent.LoadFromEnemySavedData(enemySaveData);
        }

        _cachedSavedData = null;
    }

    public void SetupSaveableRecords(List<ScriptableObject> newSaveableScriptables, List<GameObject> newSaveablePrefabs)
    {
        saveablePrefabs.Clear();
        saveableScriptableObjects.Clear();
        saveablePrefabs.AddRange(newSaveablePrefabs);
        saveableScriptableObjects.AddRange(newSaveableScriptables);
    }

    static GameSaveData _cachedSavedData;

    static Dictionary<string, ScriptableObject> _saveableScriptObjectMap = new Dictionary<string, ScriptableObject>();
    static Dictionary<string, GameObject> _saveablePrefabMap = new Dictionary<string, GameObject>();

    public static GameObject GetPrefabFromId(string id)
    {
        _saveablePrefabMap.TryGetValue(id, out GameObject value);
        return value;
    }

    public static ScriptableObject GetScriptableObjectFromId(string id)
    {
        _saveableScriptObjectMap.TryGetValue(id, out ScriptableObject value);
        return value;
    }
}
