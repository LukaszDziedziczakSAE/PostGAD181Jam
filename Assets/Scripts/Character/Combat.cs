using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] GameObject impact;

    public Vector3 impactDirection;

    public void Hit(RaycastHit hit, Vector3 origin, float damage)
    {
        impactDirection = (origin - character.Position).normalized;
        GameObject impact = Instantiate(this.impact, hit.point, Quaternion.identity);
        impact.transform.LookAt(origin);
        character.Health.TakeDamage(damage);
        //Debug.Log("Hit " + hit.ToString());
    }
}
