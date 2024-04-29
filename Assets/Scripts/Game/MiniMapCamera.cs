using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] Player player;

    private void Update()
    {
        transform.position = targetPosition;
    }

    private Vector3 targetPosition
    {
        get
        {
            if (player == null) return new Vector3(0, 10, 0);
            return new Vector3(player.Position.x, 10, player.Position.z);
        }
    }
}
