using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public float CurrentHealth => currentHealth;
    public float HealthPercentage => currentHealth / maxHealth;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        if (character.GetComponent<Player>())
        {
            UI.HealthIndicator.UpdatePlayerHealth();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        if (currentHealth <= 0)
        {
            Death();
        }

        if (character.GetComponent<Player>())
        {
            UI.HealthIndicator.UpdatePlayerHealth();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public bool IsAlive => currentHealth > 0;

    private void Death()
    {
        //character.Animator.enabled = false;
        character.Animator.SetTrigger("death");
        character.CapsuleCollider.enabled = false;
        character.Rigidbody.useGravity = false;
        /*character.Ragdoll.CollidersEnabled(true);
        character.Ragdoll.RidibodyClear();
        character.Ragdoll.AddForce(Vector3.up * 20 + -character.Combat.impactDirection * 50);
        character.Rigidbody.AddForce(Vector3.up * 20 + -character.Combat.impactDirection * 50);*/
    }
}
