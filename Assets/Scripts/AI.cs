using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
    private NavMeshAgent nav;
    public int wayPointIndex;
    public float patrolTimer;

    public enum State { Hunting, Hiding };

    public State state = State.Hunting;
    public bool Spottet = false;
    public Vector3 LastSeen;
    public float SpottedTime;

    IEnumerator Start()
    {
        nav = GetComponent<NavMeshAgent>();
        while (true)
        {
            yield return StartCoroutine(IdleState());
        }
    }

    IEnumerator IdleState()
    {
        yield return new WaitForEndOfFrame();
        switch (state)
        {
            case (State.Hiding):
                yield return StartCoroutine(HidingState());
                break;
            case (State.Hunting):
                yield return StartCoroutine(HuntingState());
                break;
            default:
                break;
        }
    }
    IEnumerator HidingState()
    {
        yield return new WaitForEndOfFrame();
    }

    IEnumerator HuntingState()
    {
        if (!Spottet)
            Patrolling();
        else
            HuntPlayer();
        yield return new WaitForEndOfFrame();
    }

    IEnumerable SpottedState()
    {
        if (Spottet)
            Flee();
        yield return new WaitForEndOfFrame();
    }

    // Script from example, simple patrolling.
    void Patrolling()
    {
        nav.speed = 2f;
        if (/*nav.destination == lastPlayerSighting.resetPosition || */nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= 1f)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
                patrolTimer = 0;
            }
        }
        else
            patrolTimer = 0;
        nav.destination = patrolWayPoints[wayPointIndex].position;
    }

    void Flee()
    {
        nav.speed = 8f;
    }

    void HuntPlayer()
    {
        Debug.Log(".,-");
        nav.speed = 8f;
        nav.SetDestination(LastSeen);
        if (SpottedTime + 5f < Time.time)
            Spottet = false;
    }

}
