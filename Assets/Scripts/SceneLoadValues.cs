using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadValues : MonoBehaviour
{
    public static SceneLoadValues Instance;
    
    [SerializeField] private List<Color> colorSchemeMalevich;
    [SerializeField] private List<Color> colorSchemeGazprom;
    
    public int currentColorSchemeID;
    public bool showID;

    private bool initialized = false;

    public List<Color> CurrentColorScheme
    {
        get
        {
            if (currentColorSchemeID == 0) return colorSchemeMalevich;
            else return colorSchemeGazprom;
        }
        private set => CurrentColorScheme = value;
    }
    
    private void Awake()
    {
        if (initialized)
        {
            foreach (var go in GameObject.FindObjectsByType<SceneLoadValues>(FindObjectsSortMode.None))
            {
                if (go != gameObject)
                {
                    Destroy(go);
                }
            }
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        initialized = true;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetColorScheme(int index)
    {
        currentColorSchemeID = index;
    }
}
