using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;
    [field: SerializeField] public Canvas Canvas;
    [SerializeField] UI_Blackscreen blackscreen;
    [SerializeField] UI_BottleCountIndicator bottleCountIndicator;
    [SerializeField] UI_HealthIndicator healthIndicator;
    [SerializeField] UI_Objective objective;
    [SerializeField] SFX_UI sfx;
    [SerializeField] UI_PauseMenu pauseMenu;
    [SerializeField] UI_Debug debug;
    
    public static UI_Blackscreen Blackscreen => Instance.blackscreen;
    public static UI_BottleCountIndicator BottleCountIndicator => Instance.bottleCountIndicator;
    public static UI_HealthIndicator HealthIndicator => Instance.healthIndicator;
    public static UI_Objective Objective => Instance.objective;
    public static SFX_UI SFX => Instance.sfx;
    public static UI_PauseMenu PauseMenu => Instance.pauseMenu;
    public static UI_Debug Debug => Instance.debug;
    
    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        Player.Instance.Input.OnMenuPress += OnMenuPress;
    }

    private void OnDisable()
    {
        Player.Instance.Input.OnMenuPress -= OnMenuPress;
    }

    private void Start()
    {
        blackscreen.gameObject.SetActive(true);
    }

    private void OnMenuPress()
    {
        if (!pauseMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            Game.ShowCursor(true);
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
            Game.ShowCursor(false);
        }
    }
}
