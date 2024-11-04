using System;
using UnityEngine;

public class PlayerSaveComponent : SaveComponent
{
    public override void InsertSaveData(GameSaveData gameSaveData)
    {
        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData.position = transform.position;
        playerSaveData.rotation = transform.rotation;   
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if (healthComponent != null)
            playerSaveData.health = healthComponent.GetHealth();

        gameSaveData.playerSaveData = playerSaveData;
    }

    internal void LoadFromPlayerSaveData(PlayerSaveData playerSaveData)
    {
        transform.position = playerSaveData.position;
        transform.rotation = playerSaveData.rotation;
        
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if (healthComponent != null)
            healthComponent.ChangeHealth(playerSaveData.health - healthComponent.GetHealth(), null);
    }
}
