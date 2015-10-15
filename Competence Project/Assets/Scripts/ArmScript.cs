using UnityEngine;
using System.Collections;

public class ArmScript : MonoBehaviour {
    public Transform obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(obj);
	}
}
