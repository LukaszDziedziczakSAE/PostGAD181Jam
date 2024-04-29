using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class AI_Sight : MonoBehaviour
{
    [SerializeField] private AI ai;
    [SerializeField] private float sightRange;
    [SerializeField] private float sightAngle;
    [SerializeField] int gizmoLines;

    public bool SeePlayer {  get; private set; }

    public event Action<Player> OnSeePlayer;

    private void Update()
    {
        //Debug.Log("Angle to player = " + AngleToCharacter(Player.Instance));
        CheckCharactersInSight();
    }

    private void CheckCharactersInSight()
    {
        if (ai.Target != null) return;

        foreach (Character character in ai.CharactersInRange)
        {
            if (!CharacterInSight(character)) return;

            if (character.TryCast<Player>(out Player player) && player.Health.IsAlive)
            {
                OnSeePlayer?.Invoke(player);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        DrawArc();
    }

    private void DrawArc()
    {
        Gizmos.color = Color.white;
        float startAngle = ai.Enemy.transform.eulerAngles.y - 90 - (sightAngle / 2);
        float endAngle = ai.Enemy.transform.eulerAngles.y - 90 + (sightAngle / 2);

        float angleIncrement = (endAngle - startAngle) / gizmoLines; // how far apart each of the lines should be
        float currentAngle = startAngle; // where to start drawing from

        for (int i = 0; i <= gizmoLines; i++)
        {
            float x = sightRange * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float y = sightRange * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector3 point = new Vector3(x, 0, -y);

            Ray ray = new Ray(transform.position, point);
            Gizmos.DrawRay(transform.position, point);
            currentAngle += angleIncrement;
        }
    }

    private bool CharacterInSight(Character character)
    {
        return Vector3.Distance(transform.position, character.Position) <= sightRange &&
            AngleToCharacter(character) >= -(sightAngle / 2) &&
            AngleToCharacter(character) <= (sightAngle / 2); 
    }

    private float AngleToCharacter(Character character)
    {
        return Vector3.Angle((character.Position - ai.Enemy.Position).normalized, ai.Enemy.transform.forward);
    }


}
