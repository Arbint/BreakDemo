using UnityEngine;

public abstract class Reward :ScriptableObject
{
    public abstract void ApplyReward(GameObject target);
}
