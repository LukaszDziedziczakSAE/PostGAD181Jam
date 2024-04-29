using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [field: SerializeField, Header("Enemy Referances")] public AI AI { get; private set; }
    [field: SerializeField] public EMode StartingMode { get; private set; }
    [field: SerializeField] public Vector3 StartingPoint { get; private set; }
    [field: SerializeField] public PatrolManager PartolManager { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public EnemyStatusIndicator StatusIndicator { get; private set; }
    [field: SerializeField, Header("Enemy Settings")] public float EngagmentRange { get; private set; } = 10f;
    [field: SerializeField] public int BurstAmount { get; private set; } = 4;
    [field: SerializeField] public float BurstRate { get; private set; } = 1.3f;

    protected override void Start()
    {
        StartingPoint = Position;
        if (WeaponManager.HasWeapons) WeaponManager.SpawnWeapon(0);
        base.Start();
    }

    public enum EMode
    {
        Inactive,
        Patrol,
        Gaurd
    }
}
