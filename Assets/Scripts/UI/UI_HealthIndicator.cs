using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthIndicator : MonoBehaviour
{
    [SerializeField] RectTransform healthIndicator;

    public void UpdatePlayerHealth()
    {
        //Debug.Log("Updating health to " + Player.Instance.Health.HealthPercentage.ToString("F2"));
        healthIndicator.localScale = new Vector3(Player.Instance.Health.HealthPercentage, 1, 1);
    }
}
