using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametersListener : MonoBehaviour, IEventReceiver<SetParametersEvent>
{
    [SerializeField] private ColorType colorType;
    private int value = 50;
    private Image image;
    
    private enum ColorType
    {
        red,
        yellow,
        green,
        blue,
    }
    
    private void Start()
    {
        image = GetComponent<Image>();
        EventBusHolder.instance.eventBus.Register(this);
    }
    

    public void OnEvent(SetParametersEvent @event)
    {
        switch (colorType)
        {
            case ColorType.red:
                value += @event.Properties.red;
                break;
            case ColorType.yellow:
                value += @event.Properties.yellow;
                break;
            case ColorType.green:
                value += @event.Properties.green;
                break;
            case ColorType.blue:
                value += @event.Properties.blue;
                break;
        }

        value = Mathf.Clamp(value, 0, 100);
        image.fillAmount = value / 100f;
    }
}
