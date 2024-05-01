using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public void StartButtonPress()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitButtonPress()
    {
        Application.Quit();
    }
}
