using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SaveUtilities : MonoBehaviour
{
    [MenuItem("Save Utilities/Generate Saveable Ids")]
    public static void GenerateSaveableIds()
    {
        string[] paths = AssetDatabase.GetAllAssetPaths();
        List<ScriptableObject> saveableScriptables = new List<ScriptableObject>();
        List<GameObject> saveablePrefabs = new List<GameObject>();
        SaveSystem saveSystem = null;

        foreach(string path in paths)
        {
            ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            ISaveable saveable = scriptableObject as ISaveable;
            if(saveable != null)
            {
                saveable.GenerateSaveableId();
                saveableScriptables.Add(scriptableObject);
                EditorUtility.SetDirty(scriptableObject);
                continue;
            }

            if (!path.Contains(".prefab"))
                continue;

            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if(gameObject != null)
            {
                SaveSystem foundSaveSystem = gameObject.GetComponent<SaveSystem>();
                if(foundSaveSystem != null)
                {
                    saveSystem = foundSaveSystem;
                    continue;
                }

                saveable = gameObject.GetComponent<ISaveable>();
                if(saveable != null)
                {
                    saveable.GenerateSaveableId();
                    EditorUtility.SetDirty(gameObject);
                    saveablePrefabs.Add(gameObject);
                    continue;
                }
            }
        }

        saveSystem.SetupSaveableRecords(saveableScriptables, saveablePrefabs);
        EditorUtility.SetDirty(saveSystem.gameObject);
        AssetDatabase.SaveAssets();
    }
}
