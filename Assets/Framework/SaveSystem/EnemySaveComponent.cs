using System;
using UnityEngine;

public class EnemySaveComponent : SaveComponent
{
    public override void InsertSaveData(GameSaveData gameSaveData)
    {
        EnemySaveData enemySaveData = new EnemySaveData();
        enemySaveData.position = transform.position;
        enemySaveData.rotation = transform.rotation;    
        
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if (healthComponent != null)
            enemySaveData.health = healthComponent.GetHealth();

        enemySaveData.prefabId = GetSaveableId();
        gameSaveData.enemySaveDatas.Add(enemySaveData);
    }

    internal void LoadFromEnemySavedData(EnemySaveData enemySaveData)
    {
        transform.position = enemySaveData.position;
        transform.rotation = enemySaveData.rotation;
        
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if (healthComponent != null)
            healthComponent.ChangeHealth(enemySaveData.health - healthComponent.GetHealth(), null);
    }
}
