using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void OnBecameVisible()
    {
        Player.Instance.Targeting.EnemyOnScreen(enemy);
    }

    private void OnBecameInvisible()
    {
        Player.Instance.Targeting.EnemyNotOnScreen(enemy);
    }
}
