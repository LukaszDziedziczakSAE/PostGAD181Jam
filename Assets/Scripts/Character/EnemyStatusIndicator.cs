using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStatusIndicator : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] TMP_Text sign;

    private void Start()
    {
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        Vector3 rot = GetComponent<RectTransform>().eulerAngles;
        rot = new Vector3(-rot.x, rot.y + 180, rot.z);
        GetComponent<RectTransform>().eulerAngles = rot;
    }

    public void Show(bool alert)
    {
        if (alert)
        {
            sign.text = "!";
        }
        else
        {
            sign.text = "?";
        }
    }

    public void Hide()
    {
        sign.text = "";
    }
}
