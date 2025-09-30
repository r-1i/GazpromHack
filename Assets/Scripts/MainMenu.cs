using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetColorScheme(int index)
    {
        SceneLoadValues.Instance.SetColorScheme(index);
    }

    public void OnShowIDCardChanged(bool show)
    {
        SceneLoadValues.Instance.showID = show;
    }
}
