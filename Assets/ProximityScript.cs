using UnityEngine;
using System.Collections;

public class ProximityScript : MonoBehaviour {


    private NavMeshAgent nav;
    private Vector3 p;


	// Use this for initialization
	void Start () {
        nav = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        p = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        float l = CalculatePathLength(p);
        Debug.Log(l > 20);
        if (l > 20)
            SetWhite();
        else
            SetRed();
	}

    void SetRed()
    {
        RenderSettings.fogColor = Color.red;
        RenderSettings.fog = true;
    }

    void SetWhite()
    {
        RenderSettings.fogColor = Color.white;
        RenderSettings.fog = true;
    }


    // Unity's function as well..
    float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
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

}
