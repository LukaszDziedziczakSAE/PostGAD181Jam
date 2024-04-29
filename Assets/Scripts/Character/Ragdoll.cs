using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    [SerializeField] Rigidbody[] rigidbodies;

    private void Start()
    {
        CollidersEnabled(false);
    }

    public void CollidersEnabled(bool enabled)
    {
        foreach(Collider collider in colliders)
        {
            collider.enabled = enabled;
        }
    }

    public void RidibodyEnabled(bool enabled)
    {
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            //rigidbodies
        }


    }

    public void RidibodyClear()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.ResetInertiaTensor();
        }
    }

    public void AddForce(Vector3 force)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddForce(force, ForceMode.Impulse);
        }


    }
}
