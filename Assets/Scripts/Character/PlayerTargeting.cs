using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    [SerializeField] float targetingRange;

    List<Enemy> visableEnemies = new List<Enemy>();

    [field: SerializeField] public Enemy Target { get; private set; }

    public bool HasTarget => Target != null;

    private void Update()
    {
        SetTarget();
    }

    public void EnemyOnScreen(Enemy enemy)
    {
        visableEnemies.Add(enemy);
        //Debug.Log(enemy.name + " visable on screen");
    }

    public void EnemyNotOnScreen(Enemy enemy)
    {
        //Debug.Log(enemy.name + " not visable on screen");
        if (visableEnemies.Contains(enemy)) visableEnemies.Remove(enemy);
    }

    private void SetTarget()
    {
        
        if (visableEnemies.Count > 0)
        {
            Enemy closest = null;
            float closestDistance = Mathf.Infinity;

            foreach (Enemy enemy in visableEnemies)
            {
                float distance = Vector3.Distance(enemy.Position, Player.Instance.Position);
                if (distance < closestDistance)
                {
                    closest = enemy;
                    closestDistance = distance;
                }
            }

            if (Vector3.Distance(closest.Position, Player.Instance.Position) < targetingRange)
            {
                Target = closest;
            }
            else Target = null;
        }
        else Target = null;
    }
}
