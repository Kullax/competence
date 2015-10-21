using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SavedValues : MonoBehaviour {
    private Vector3 pos;
    private List<GameObject> enemies;
    public GameObject[] spawnpoints;
    private GameObject[] waypoints;
    private GameObject[] computers;
    public bool ArmLooted = false;
    private GameObject player;
    private NavMeshAgent nav;
    public int KillCount;
    public int KillLimit = 6;
    public bool Paused = false;

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        pos = new Vector3 (0,0,0);
        enemies = new List<GameObject>();
        GameObject[] BadGuys = GameObject.FindGameObjectsWithTag("Enemy");
        spawnpoints = GameObject.FindGameObjectsWithTag("Spawner");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        computers = GameObject.FindGameObjectsWithTag("Computer");

        foreach (var enemy in BadGuys)
        {
            enemies.Add(enemy);
        }

        nav = player.GetComponent<NavMeshAgent>();
    }

    public void setPoint(Vector3 newPoint)
    {
        pos = newPoint;
    }

    public Vector3 getPoint()
    {
        return pos;
    }

    public List<GameObject> getEnemies()
    {
        return enemies;
    }

    public void addEnemy(GameObject BadGuy)
    {
        enemies.Add(BadGuy);
    }

    public void removeEnemy(GameObject BadGuy)
    {
        enemies.Remove(BadGuy);
        Destroy(BadGuy);
        KillCount += 1;
    }

    public int enemiesLength()
    {
        return enemies.Count;
    }

    public int waypointLengt()
    {
        return waypoints.Length;
    }

    public Transform[] getWayPoints()
    {
        return waypoints.OrderBy(item => Random.Range(-1, 1)).Select(item => item.transform).ToArray();
    }
    
    // Unity's example function..
    public float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the player position.
        allWayPoints[0] = player.transform.position;

        // The last point is the enemy position
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

    public int ActiveComputers()
    {
        int o = 0;
        foreach (var computer in computers)
        {
            if (! computer.GetComponent<InteractScript>().done)
            {
                o += 1;
            }
        }
        return o;
    }

}