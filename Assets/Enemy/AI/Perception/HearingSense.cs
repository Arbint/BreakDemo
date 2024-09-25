using System;
using UnityEngine;

public class HearingSense : Sense
{
    public delegate void OnSoundEventSentDelegate(float volume, Stimuli stimuli);

    public static event OnSoundEventSentDelegate OnSoundEventSent;

    public static void SendSoundEvent(float volume, Stimuli stimuli)
    {
        OnSoundEventSent?.Invoke(volume, stimuli);
    }

    private void Awake()
    {
        OnSoundEventSent += HandleSoundEvent;
    }

    private void HandleSoundEvent(float volume, Stimuli stimuli)
    {
        Debug.Log($"Handling hearing event with volume: {volume} and stimuli: {stimuli.gameObject.name}");
    }

    protected override bool IsStimuliSensible(Stimuli stimuli)
    {
        return false;
    }
}
