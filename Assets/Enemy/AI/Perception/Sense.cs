using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    [SerializeField] private bool bDrawDebug = true;
    
    private static HashSet<Stimuli> _registeredStimuliSet = new HashSet<Stimuli>();

    private HashSet<Stimuli> _currentSensibleStimuliSet = new HashSet<Stimuli>();
    public static void RegisterStimuli(Stimuli stimuli)
    {
        _registeredStimuliSet.Add(stimuli);
    }

    public static void UnRegisterStimuli(Stimuli stimuli)
    {
        _registeredStimuliSet.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensible(Stimuli stimuli);
    
    private void Update()
    {
        foreach(Stimuli stimuli in _registeredStimuliSet)
        {
            if (IsStimuliSensible(stimuli))
            {
                HandleSensibleStimuli(stimuli);
            }
            else
            {
                HandleNoSensibleStimuli(stimuli);
            }
        }
    }

    private void HandleNoSensibleStimuli(Stimuli stimuli)
    {
        // we can't sense it now, but also we did not sense it before, noting needs to be done
        if (!_currentSensibleStimuliSet.Contains(stimuli))
            return;

        _currentSensibleStimuliSet.Remove(stimuli);
        Debug.Log($"I just lost track of: {stimuli.gameObject.name}");
    }

    private void HandleSensibleStimuli(Stimuli stimuli)
    {
        // we can sense it now, but we also can sense it before, nothing needs to be done
        if (_currentSensibleStimuliSet.Contains(stimuli))
            return;

        _currentSensibleStimuliSet.Add(stimuli);
        Debug.Log($"I just sensed: {stimuli.gameObject.name}");
    }

    private void OnDrawGizmos()
    {
        if (bDrawDebug)
        {
            OnDrawDebug();
        }
    }

    protected virtual void OnDrawDebug()
    {
       //override in child class to draw debug 
    }
}
