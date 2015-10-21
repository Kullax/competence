using System;
using System.Security.Policy;
using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    private NavMeshAgent nav;
    public Transform[] wayPointArray;
    public int wayPointIndex;
    public float patrolTimer;

    public enum State { Hunting, Hiding };

    public State state = State.Hunting;
    public bool Spotted = false;
    public Vector3 LastSeenPosition;
    public float SpottedTime;
    public TextMesh text;
    private float marktimer = 0;
    private SavedValues sv;

    private Action _playScare;

    IEnumerator Start()
    {
        sv = FindObjectOfType<SavedValues>();
        wayPointArray = sv.getWayPoints();
        nav = GetComponent<NavMeshAgent>();

        _playScare = Camera.main.GetComponent<SoundScript>().PlayAware;

        while (true)
            yield return StartCoroutine(HuntingState());
    }

    IEnumerator HuntingState()
    {
        marktimer += Time.deltaTime;
        if(marktimer > 2f)
            text.text = "";
        if (!Spotted)
            Patrolling();
        else
            HuntPlayer();
        yield return null;
    }

    // Script from example, simple patrolling.
    void Patrolling()
    {
        text.text = "";
        nav.speed = 2f;
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= 1f)
            {
                if (wayPointIndex == wayPointArray.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
                patrolTimer = 0;
            }
        }
        else
            patrolTimer = 0;
        nav.destination = wayPointArray[wayPointIndex].transform.position;
    }

    void HuntPlayer()
    {
        nav.speed = 8f;
        if (Time.time - SpottedTime < 0.20f)
            nav.SetDestination(LastSeenPosition);
        if (Time.time - SpottedTime > 5f)
            Spotted = false;
    }

    public void SpottedPlayer()
    {
        if (Spotted)
            return;

        marktimer = 0;
        Spotted = true;
        
        // Do the MGS
        text.text = "!";
        _playScare();

        // Alert mah friends
        foreach (var pacman in sv.getEnemies())
            ScreamAt(pacman);
    }

    private void ScreamAt(GameObject friend)
    {
        // Can our friend hear us?
        var dist = sv.CalculatePathLength(nav, friend.transform.position);
        if (dist < 70)
        {
            Debug.Log("I hear something!");
            var ai = friend.GetComponent<AI>();
            if (ai == null || ai.Spotted)
                // either friend lost his brain, or he already knows about the player, so don't scream
                return;

            ai.SpottedPlayer();
            ai.SpottedTime = SpottedTime;
            ai.LastSeenPosition = transform.position;
        }
    }
}
