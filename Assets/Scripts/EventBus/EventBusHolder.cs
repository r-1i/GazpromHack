using System;
using UnityEngine;

public class EventBusHolder : MonoBehaviour
{
    public static EventBusHolder instance;
    public EventBus eventBus { get; private set; }
    
    private void Awake()
    {
        instance = this;
        eventBus = new EventBus();
    }
}
