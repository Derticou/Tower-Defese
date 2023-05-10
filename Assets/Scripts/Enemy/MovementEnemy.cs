using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public float startSpeed = 10;

    [HideInInspector]
    public  float speed;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        speed = startSpeed;
        target = Waypoints.points[0];
    }

    public void Slow(float amount)
    {
        speed = speed * (1f - amount);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position,target.position)<=0.2f)
        {
            GetNextWaypoint();
        }

        speed = startSpeed;
    }
    void GetNextWaypoint()
    {
        if (wavepointIndex>=Waypoints.points.Length-1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
