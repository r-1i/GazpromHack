using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorImage : MonoBehaviour
{
    [SerializeField] public int index;

    private void Awake()
    {
        GetComponent<Image>().color = SceneLoadValues.Instance.CurrentColorScheme[index];
    }
}
