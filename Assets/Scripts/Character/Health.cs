using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public bool IsAlive => currentHealth > 0;

    private void Death()
    {
        character.Animator.enabled = false;
        character.Ragdoll.CollidersEnabled(true);
        character.CapsuleCollider.enabled = false;
        character.Ragdoll.RidibodyClear();
        character.Ragdoll.AddForce(Vector3.up * 20 + -character.Combat.impactDirection * 50);
        //character.Rigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }
}
