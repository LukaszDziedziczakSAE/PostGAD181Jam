using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Character character;
    WeaponConfig config;
    public bool TriggerPulled = false;
    float lastFire;
    [SerializeField] ParticleSystem muzzleEffect;
    [field: SerializeField] public SFX_Weapon SFX {  get; private set; }
    //[SerializeField] GameObject Impact;
    [SerializeField] Transform muzzle;
    [SerializeField] LayerMask hitableLayers;


    public event Action OnFire;

    private void Update()
    {
        if (config == null || character == null) return;
        if (Time.time - lastFire < FireRate) return;

        if (TriggerPulled)
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        //Debug.LogWarning("Weapon Fire");
        lastFire = Time.time;
        muzzleEffect?.Play();
        SFX?.PlayerFireSFX();

        Ray ray = new Ray(muzzle.position, muzzle.forward);
        Debug.DrawRay(muzzle.position, muzzle.forward * 50f, Color.red, 5);

        RaycastHit[] hits = Physics.RaycastAll(ray, 50f);
        if (hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                Debug.Log(character.name + " hit " + hit.collider.name);

                if (hit.collider.TryGetComponent<Character>(out Character hitCharacter))
                {
                    hitCharacter.Combat.Hit(hit, muzzle.transform.position, config.Damage);
                }
            }
        }
        else 
        {
            Debug.Log("No Hit"!);
        }

        OnFire?.Invoke();
    }

    public float FireRate => config.FireRate;

    public void Initilise(WeaponConfig weaponConfig)
    {
        config = weaponConfig;
        transform.localPosition = config.SpawnPosition;
        transform.localEulerAngles = config.SpawnRotation;
        character = GetComponentInParent<Character>();
    } 
}
