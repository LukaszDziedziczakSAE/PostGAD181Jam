using UnityEngine;

public class PatrolWaypoint : MonoBehaviour
{
    [SerializeField] float gizmoRadius = 0.5f;
    [SerializeField] float waitTimeBase = 1f;
    [SerializeField] float waitTimeVariation = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, gizmoRadius);
    }

    public Vector3 Position => transform.position;

    public float WaitTime => Random.Range(waitTimeBase - waitTimeVariation, waitTimeBase + waitTimeVariation);
}
