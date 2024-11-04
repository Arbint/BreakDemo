using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void SaveGame()
    {
        GameSaveData gameSaveData = new GameSaveData(); 
        foreach(SaveComponent saveComponet in GameObject.FindObjectsByType<SaveComponent>(FindObjectsSortMode.None))
        {
            saveComponet.InsertSaveData(gameSaveData);
        }
    }
}
