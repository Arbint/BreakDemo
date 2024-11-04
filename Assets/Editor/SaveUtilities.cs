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
            }
        }
    }
}
