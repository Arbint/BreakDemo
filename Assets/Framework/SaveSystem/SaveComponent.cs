using UnityEngine;

public abstract class SaveComponent : MonoBehaviour
{
    public string GetSaveableId()
    {
        ISaveable saveable = GetComponent<ISaveable>();
        if(saveable != null)
        {
            return saveable.GetSaveableId();
        }

        return "";
    }

    public abstract void InsertSaveData(GameSaveData gameSaveData);
}
