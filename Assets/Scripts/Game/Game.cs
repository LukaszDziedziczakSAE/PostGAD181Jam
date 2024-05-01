using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] Player Player;
    [SerializeField] CameraController cameraController;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float groundRaycastHeight;
    [SerializeField] float minFallHeight = 0.5f;
    [SerializeField] float timeToRespawnAfterDeath = 2f;
    [SerializeField] BottleCollectionWin collectionWin;
    [SerializeField] Music music;

    float timer;

    public static LayerMask GroundLayers => Instance.groundLayers;
    public static float GroundRaycastHeight => Instance.groundRaycastHeight;
    public static float MinFallHeight => Instance.minFallHeight;
    public static BottleCollectionWin BottleCollectionWin => Instance.collectionWin;
    public static Music Music => Instance.music;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!Player.Health.IsAlive) timer += Time.deltaTime;

        if (timer >= timeToRespawnAfterDeath && UI.Blackscreen.IsClear)
        {
            BeginLevelReset();
        }
    }

    public static void BeginLevelReset()
    {
        UI.Blackscreen.Toggle();
        UI.Blackscreen.OnFinish += ResetLevel;
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static CameraController CameraController => Instance.cameraController;
}
