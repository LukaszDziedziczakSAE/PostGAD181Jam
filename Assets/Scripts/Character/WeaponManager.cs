using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Character character;
    public Weapon CurrentWeapon {  get; private set; } 
    [field: SerializeField] public WeaponConfig[] Weapons { get; private set; }
    [field: SerializeField] public int Index { get; private set; } = -1;

    public WeaponConfig CurrentWeaponConfig
    {
        get
        {
            if (Index == -1) return null;
            return Weapons[Index];
        }
    }

    public void SpawnWeapon(int index)
    {
        if (!HasWeapons || index < 0 || index >= Weapons.Length) return;

        if (Index != -1) DespawnWeapon();

        Index = index;
        CurrentWeapon = Instantiate(CurrentWeaponConfig.WeaponPrefab, character.RightHand);
        CurrentWeapon.Initilise(CurrentWeaponConfig);
    }

    public void DespawnWeapon()
    {
        Destroy(CurrentWeapon);
        Index = -1;
    }

    public bool HasWeapons => Weapons.Length > 0;
    public bool WeaponSpwaned => CurrentWeapon != null;
}
