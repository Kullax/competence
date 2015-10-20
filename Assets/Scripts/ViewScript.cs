using UnityEngine;
using System.Collections;

public class ViewScript : MonoBehaviour {
    SphereCollider col;
    AI AIScript;
    [Range(0,360)]
    public float fieldOfView = 0;
	// Use this for initialization
	void Start () {
        col = GetComponent<SphereCollider>();
        AIScript = GetComponent<AI>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        Vector3 direction = collider.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < fieldOfView * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawLine(transform.position, collider.transform.position);
                    AIScript.Spottet = true;
                    AIScript.SpottedTime = Time.time;
                    AIScript.LastSeen = collider.transform.position;
                }
            }
        }
    }
}
