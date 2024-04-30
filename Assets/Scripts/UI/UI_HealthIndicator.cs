using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthIndicator : MonoBehaviour
{
    [SerializeField] RectTransform healthIndicator;

    private void Start()
    {
        UpdatePlayerHealth();
    }

    public void UpdatePlayerHealth()
    {
        healthIndicator.localScale = new Vector3(Player.Instance.Health.HealthPercentage, 0, 0);
    }
}
