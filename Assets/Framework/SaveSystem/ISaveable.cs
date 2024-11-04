using UnityEngine;

public interface ISaveable
{
    public string GetSaveableId();
    public void GenerateSaveableId();
}
