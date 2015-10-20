using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    private NavMeshAgent nav;
    //public Transform[] wayPointArray;
    public int wayPointIndex;
    public float patrolTimer;

    public enum State { Hunting, Hiding };

    public State state = State.Hunting;
    public bool Spottet = false;
    public Vector3 LastSeen;
    public float SpottedTime;
    public TextMesh text;
    private float marktimer = 0;
    private SavedValues sv;

    IEnumerator Start()
    {
        sv = GameObject.FindObjectOfType<SavedValues>();
        nav = GetComponent<NavMeshAgent>();
      //  Debug.Log(wayPointArray.Length);
        Debug.Log(sv.waypoints.Length);
//        wayPointArray = new Transform[sv.waypoints.Length];
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
        marktimer += Time.deltaTime;
        if(marktimer > 2f)
        {
            text.text = "";
        }
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
        text.text = "";
        nav.speed = 2f;
        if (/*nav.destination == lastPlayerSighting.resetPosition || */nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= 1f)
            {
                if (wayPointIndex == sv.waypoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
                patrolTimer = 0;
            }
        }
        else
            patrolTimer = 0;
        nav.destination = sv.waypoints[wayPointIndex].transform.position;
    }

    void Flee()
    {
        nav.speed = 8f;
    }

    void HuntPlayer()
    {
        nav.speed = 8f;
        nav.SetDestination(LastSeen);
        if (SpottedTime + 5f < Time.time)
            Spottet = false;
    }

    public void SpottedPlayer()
    {
        marktimer = 0;
        Spottet = true;
        
        text.text = "!";
    }

}
