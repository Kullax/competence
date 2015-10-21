using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    Text enemies;
    Text computers;
    Text objective;
    SavedValues sv;
    // Use this for initialization
    void Start () {
        Text[] ts = GetComponentsInChildren<Text>();
        foreach (var t in ts)
        {
            if (t.name == "Enemies")
                enemies = t;
            if (t.name == "Computers")
                computers = t;
        }
        sv = GameObject.FindObjectOfType<SavedValues>();
    }
	
	// Update is called once per frame
	void Update () {
        enemies.text = "Kills remaining: " + (sv.KillLimit-sv.KillCount);
        computers.text = "Hacks remaining: " + sv.ActiveComputers();
	}
}
