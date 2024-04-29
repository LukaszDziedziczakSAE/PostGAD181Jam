using UnityEngine;

public class PatrolManager : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] PatrolWaypoint[] waypoints;
    [SerializeField] float waypointProximity = 0.5f;
    [Header("Debug")]
    [SerializeField] int index = -1;
    [SerializeField] bool backward;
    bool forward 
    { 
        get { return !backward; } 
        set { backward = !value; } 
    }
    float timer = 0;

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    public PatrolWaypoint CurrentWaypoint
    {
        get
        {
            if (index < 0 || index >= waypoints.Length) return null;
            return waypoints[index];
        }
    }

    public bool HasWaypoints => waypoints.Length > 0;

    public bool InProximityToCurrentWaypoint
    {
        get
        {
            if (CurrentWaypoint == null) return false;
            bool isAtWaypoint = DistanceToCurrentWaypoint <= waypointProximity;
            //if (isAtWaypoint) Debug.Log("At way point");
            return isAtWaypoint;
        }
    }

    public float DistanceToCurrentWaypoint
    {
        get
        {
            return Vector3.Distance(enemy.Position, CurrentWaypoint.Position);
        }
        
    }

    public bool IsWaiting => timer > 0;

    public bool Unset => index == -1;

    public void SetNextWaypoint()
    {
        if (index < 0)
        {
            index = 0;
            // TODO: find nearest waypoint and go there
            return;
        }

        if (forward)
        {
            if (index + 1 >= waypoints.Length)
            {
                timer = CurrentWaypoint.WaitTime;
                index--;
                backward = true;
            }
            else
            {
                timer = CurrentWaypoint.WaitTime;
                index++;
            }
        }
        else // backward == true
        {
            if (index - 1 < 0)
            {
                timer = CurrentWaypoint.WaitTime;
                index++;
                forward = true;
            }
            {
                timer = CurrentWaypoint.WaitTime;
                index--;
            }
        }
    }

    public Vector2 CurrentWaypointDirectionNormalised
    {
        get
        {
            Vector3 direction = CurrentWaypoint.Position - enemy.Position;
            //print("direction = " + new Vector2(direction.x, direction.z).normalized);
            return new Vector2(direction.x, direction.z).normalized;
        }
    }

    public void ResetTimer()
    {
        timer = 0;
    }
}
