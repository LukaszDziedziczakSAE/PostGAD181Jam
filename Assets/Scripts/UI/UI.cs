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
    
    public static UI_Blackscreen Blackscreen => Instance.blackscreen;
    public static UI_BottleCountIndicator BottleCountIndicator => Instance.bottleCountIndicator;
    public static UI_HealthIndicator HealthIndicator => Instance.healthIndicator;
    
    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        blackscreen.gameObject.SetActive(true);
    }
}
