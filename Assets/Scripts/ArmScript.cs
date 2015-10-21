using UnityEngine;
using System.Collections;

public class ArmScript : MonoBehaviour {
    public Transform obj;
    SavedValues sv;

	// Use this for initialization
	void Start () {
        sv = FindObjectOfType<SavedValues>();
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(obj);
	}
}
