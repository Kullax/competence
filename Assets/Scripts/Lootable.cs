using UnityEngine;
using System.Collections;

public class Lootable : MonoBehaviour {
    SavedValues sv;
    public GameObject effect;

    // Use this for initialization
    void Start () {
        sv = GameObject.FindObjectOfType<SavedValues>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 1, 0);
	}


    void OnTriggerStay(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        sv.ArmLooted = true;
        effect.SetActive(true);
        Destroy(this.gameObject);
    }

}
