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
    [SerializeField] Collider trigger;
    [SerializeField] ParticleSystem smashPrefab;
    [SerializeField] float homingSpeed;
    [SerializeField] float damage = 100;
    [SerializeField] float homingDelay = 0.5f;
    [SerializeField] float targetHeightOffset = 1.7f;
    [SerializeField] GameObject pickUpEffect;
    [SerializeField] float moveTooFastDistance = 2f;

    Enemy target;
    float throwStartTime = 0;
    float homeingFlightTime => Time.time - throwStartTime;
    Vector3 throwPosition;
    Vector3 lastPosition;

    Vector3 inHandPoistion => Player.Instance.HandPosition + spawnPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }


    public enum EMode
    {
        Pickup,
        Throwing,
        InFlight,
        Homing
    }

    private void Update()
    {
        if ((mode == EMode.InFlight || mode == EMode.Homing))
        {
            if (Vector3.Distance(transform.position, lastPosition) > moveTooFastDistance)
            {
                transform.position = lastPosition;
                Debug.LogWarning(name + " moving too fast");
            }
        }

        if (mode == EMode.Homing && homeingFlightTime > homingDelay)
        {
            Vector3 targetPosition = new Vector3(target.Position.x, target.Position.y + targetHeightOffset, target.Position.z);
            Vector3 direction = (targetPosition - transform.position).normalized;
            Vector3 position = transform.position;
            position += direction * homingSpeed * Time.deltaTime;
            transform.position = position;
        }

        /*else if (mode == EMode.Throwing)
        {
            //transform.position = inHandPoistion;
            transform.localPosition = spawnPosition;
            transform.localEulerAngles = spawnRotation;
        }*/

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DoNotHit")return;

        if (Time.time > 0.5f && other.tag != "Player") Debug.Log(name + " hit " + other.gameObject.name);
      
        if (mode == EMode.Pickup)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Inventory.AddBottleToInventory();
                Game.BottleCollectionWin.BottlePickedUp();
                UI.SFX.PlayPickUpSound();
                if (pickUpEffect != null) Instantiate(pickUpEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        else if (mode == EMode.InFlight && other.tag != "Player")
        {

            //Debug.Log("bottle hit " + other.gameObject.name);
            ParticleSystem smash = Instantiate(smashPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        else if (mode == EMode.Homing && other.tag != "Player")
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

    public void SetModeThrowing(/*Player player*/)
    {
        mode = EMode.Throwing;
        _collider.enabled = false;
        //transform.parent = player.RightHand;
        //transform.localPosition = spawnPosition;
        //transform.localEulerAngles = spawnRotation;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void SetModeInFlight(Vector3 newVelocity)
    {
        mode = EMode.InFlight;

        _collider.enabled = true;
        throwPosition = transform.position;
        transform.parent = null;

        rb.ResetInertiaTensor();
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = newVelocity;
        trigger.enabled = true;
    }

    public void SetModeTargeted(Enemy enemy, Vector3 newVelocity)
    {
        //Debug.Log(name + " in targeting mode");
        target = enemy;
        mode = EMode.Homing;

        _collider.enabled = false;
        throwPosition = transform.position;
        transform.parent = null;

        rb.constraints = RigidbodyConstraints.None;
        rb.ResetInertiaTensor();
        rb.velocity = newVelocity;
        throwStartTime = Time.time;
        trigger.enabled = true;
    }

    private void BeginHoming()
    {
        
        rb.useGravity = false;
    }
}
