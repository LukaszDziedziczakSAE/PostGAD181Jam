using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;
    [field: SerializeField] public Canvas Canvas;
    [SerializeField] UI_Blackscreen blackscreen;
    public static UI_Blackscreen Blackscreen => Instance.blackscreen;

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
}
