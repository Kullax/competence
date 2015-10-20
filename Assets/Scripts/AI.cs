using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
    private NavMeshAgent nav;
    public int wayPointIndex;
    public float patrolTimer;

    public enum State { Hunting, Hiding };

    public State state = State.Hunting;

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
        Patrolling();
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

}
