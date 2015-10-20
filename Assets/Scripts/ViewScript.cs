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
	
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        Vector3 direction = collider.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        RaycastHit hit;
        if (angle < fieldOfView * 0.5f)
        {
            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
            {
                if (hit.collider.tag == "Player")
                {
                    if (!AIScript.Spottet)
                    {
                        AIScript.SpottedPlayer();
                        Camera cam = GameObject.FindObjectOfType<Camera>();
                        SoundScript script = cam.GetComponent<SoundScript>();
                        script.PlayAware();
                    }
                    AIScript.SpottedTime = Time.time;
                    AIScript.LastSeen = collider.transform.position;
                }
            }
        }

        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 3))
        {
            if (hit.collider.tag == "Player")
            {
                if (!AIScript.Spottet)
                {
                    GameObject.FindObjectOfType<SavedValues>().removeEnemy(this.gameObject);
                }else
                {
                    Debug.Log("you dead");
                }
            }
        }
    }
}
