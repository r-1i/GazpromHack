using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public CanvasGroup leftDescription;
    public CanvasGroup rightDescription;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
