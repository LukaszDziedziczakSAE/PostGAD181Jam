using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance {  get; private set; }
    [field: SerializeField, Header("Player Referances")] public InputReader Input { get; private set; }
    [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    [field: SerializeField] public BottleThrower BottleThrower { get; private set; }
    [field: SerializeField] public PlayerTargeting Targeting { get; private set; }
    private void Awake()
    {
        if (Player.Instance == null) Instance = this;
        else Destroy(this.gameObject);

    }

    /// <summary>
    /// Relative to the direction of where camera is pointing the character's forward movement
    /// </summary>
    public Vector3 CamFoward
    {
        get
        {
            Vector2 flatPos = CameraController.CirclePosition(1, Game.CameraController.Angle2Player + 180);
            return transform.position - new Vector3(flatPos.x, transform.position.y, flatPos.y); 
        }
    }

    /// <summary>
    /// Relative to the direction of where camera is pointing the character's right movement
    /// </summary>
    public Vector3 CamRight
    {
        get
        {
            Vector2 flatPos = CameraController.CirclePosition(1, Game.CameraController.Angle2Player + 180 - 90);
            return transform.position - new Vector3(flatPos.x, transform.position.y, flatPos.y);
        }
    }
}
