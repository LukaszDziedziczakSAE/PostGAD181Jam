using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public Weapon WeaponPrefab { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; } = 0.1f;
    [field: SerializeField] public float Damage { get; private set; } = 10;
    [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
    [field: SerializeField] public Vector3 SpawnRotation { get; private set; }

}
