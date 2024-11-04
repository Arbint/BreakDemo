using UnityEngine;
using UnityEditor;

public class SaveUtilities : MonoBehaviour
{
    [MenuItem("Save Utilities/Generate Saveable Ids")]
    public static void GenerateSaveableIds()
    {
        string[] paths = AssetDatabase.GetAllAssetPaths();
        foreach(string path in paths)
        {
            ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            ISaveable saveable = scriptableObject as ISaveable;
            if(saveable != null)
            {
                saveable.GenerateSaveableId();
                continue;
            }

            if (!path.Contains(".prefab"))
                continue;

            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if(gameObject != null)
            {
                saveable = gameObject.GetComponent<ISaveable>();
                if(saveable != null)
                {
                    saveable.GenerateSaveableId();
                    continue;
                }
            }
        }
    }
}
