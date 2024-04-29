using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject camFocus;
    [SerializeField] float distanceToPlayer = 10f;
    [SerializeField] float angle = 60f;
    [SerializeField] float characterHeightOffeset = 1.6f;
    [SerializeField] float rotationRate;

    [SerializeField] float angleToPlayer = 0;

    public float Angle2Player => angleToPlayer;

    private void Update()
    {
        if (camFocus == null) return;
        if (!camFocus.GetComponent<Player>().Health.IsAlive) return;
        transform.position = targetPosition;
        transform.eulerAngles = new Vector3(angle, 360 - angleToPlayer, 0);
    }

    private Vector3 targetPosition
    {
        get
        {
            if (camFocus == null)
            {
                Debug.LogError(name + ": missing player referance");
                return Vector3.zero;
            }

            float radious = (Mathf.Cos(angle * Mathf.Deg2Rad) * distanceToPlayer);
            float height = characterHeightOffeset + (Mathf.Sin(angle * Mathf.Deg2Rad) * distanceToPlayer);

            Vector2 flatPos = CirclePosition(radious, angleToPlayer + 180);

            float x = camFocus.transform.position.x + flatPos.x;
            float y = camFocus.transform.position.y + height;
            float z = camFocus.transform.position.z + flatPos.y;
            return new Vector3(x, y, z);
        }
    }

    public static Vector2 CirclePosition(float radius, float angle)
    {
        float x = radius * Mathf.Cos((angle + 90) * Mathf.PI / 180);
        float y = radius * Mathf.Sin((angle + 90)* Mathf.PI / 180);
        return new Vector2(x, y);
    }

    public void Rotate(float angle)
    {
        angleToPlayer += (angle * rotationRate * Time.deltaTime);

        if (angleToPlayer < -180) angleToPlayer += 360;
        else if (angleToPlayer > 180) angleToPlayer -= 360;
    }
}
