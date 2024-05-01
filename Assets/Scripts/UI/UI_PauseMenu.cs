using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Button resetButton;

    private void OnEnable()
    {
        eventSystem.firstSelectedGameObject = resetButton.gameObject;
    }

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
