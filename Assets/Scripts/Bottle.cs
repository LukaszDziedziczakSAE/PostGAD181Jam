using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private EMode mode;

    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 spawnRotation;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider _collider;
    [SerializeField] ParticleSystem smashPrefab;
    [SerializeField] float homingSpeed;
    [SerializeField] float damage = 100;
    [SerializeField] float homingDelay = 0.5f;
    [SerializeField] float targetHeightOffset = 1.7f;

    Enemy target;
    float throwStartTime = Mathf.NegativeInfinity;
    float homeingFlightTime => Time.time - throwStartTime;

    public enum EMode
    {
        Pickup,
        Throwing,
        InFlight,
        Homing
    }

    private void Update()
    {
        if (mode == EMode.Homing && homeingFlightTime > homingDelay)
        {
            Vector3 targetPosition = new Vector3(target.Position.x, target.Position.y + targetHeightOffset, target.Position.z);
            Vector3 direction = (targetPosition - transform.position).normalized;
            Vector3 position = transform.position;
            position += direction * homingSpeed * Time.deltaTime;
            transform.position = position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DoNotHit")return;

        if (Time.time > 0.5f) Debug.Log(name + " hit " + other.gameObject.name);
      
        if (mode == EMode.Pickup)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Inventory.AddBottleToInventory();
                Destroy(gameObject);
            }
        }

        else if (mode == EMode.InFlight)
        {

            //Debug.Log("bottle hit " + other.gameObject.name);
            ParticleSystem smash = Instantiate(smashPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        else if (mode == EMode.Homing)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health == null) health = other.GetComponentInParent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
            else Debug.LogError(name + " did not find enemy on impact");
            
            ParticleSystem smash = Instantiate(smashPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    public void SetModeThrowing(Player player)
    {
        mode = EMode.Throwing;
        _collider.enabled = false;
        transform.parent = player.RightHand;
        transform.localPosition = spawnPosition;
        transform.localEulerAngles = spawnRotation;
    }

    public void SetModeInFlight(Vector3 newVelocity)
    {
        mode = EMode.InFlight;
        _collider.enabled = true;
        transform.parent = null;
        rb.velocity = newVelocity;
    }

    public void SetModeTargeted(Enemy enemy, Vector3 newVelocity)
    {
        //Debug.Log(name + " in targeting mode");
        target = enemy;
        mode = EMode.Homing;
        _collider.enabled = true;
        transform.parent = null;
        rb.velocity = newVelocity;
        throwStartTime = Time.time;
    }

    private void BeginHoming()
    {
        
        rb.useGravity = false;
    }
}
