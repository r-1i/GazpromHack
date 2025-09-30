using UnityEngine;

public readonly struct RedEvent : IEvent
{
    public readonly Vector3 MoveDelta;

    public RedEvent(Vector3 moveDelta)
    {
        MoveDelta = moveDelta;
    }
    
}