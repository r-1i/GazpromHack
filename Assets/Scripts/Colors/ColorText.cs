using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorText : MonoBehaviour
{
    [SerializeField] public int index;

    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().color = SceneLoadValues.Instance.CurrentColorScheme[index];
    }
}
