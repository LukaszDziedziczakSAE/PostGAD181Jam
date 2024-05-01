using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{

    public void OnResetPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void OnExitPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}
