using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    Text enemies;
    Text computers;
    Text objective;
    Text victory;
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
            if (t.name == "Victory")
                victory = t;
        }
        victory.GetComponentInChildren<Button>().GetComponentInChildren<Text>().enabled= false;
        victory.GetComponentInChildren<Button>().GetComponentInChildren<Image>().enabled = false;
        victory.GetComponentInChildren<Button>().enabled = false;
        victory.enabled = false;
        sv = GameObject.FindObjectOfType<SavedValues>();
    }
	
	// Update is called once per frame
	void Update () {
        enemies.text = "Kills remaining: " + (sv.KillLimit-sv.KillCount);
        computers.text = "Hacks remaining: " + sv.ActiveComputers();
	}

    public void ShowVictory()
    {
//        victory.GetComponentInChildren<Button>().enabled = true;
        victory.GetComponentInChildren<Button>().GetComponentInChildren<Text>().enabled = true;
        victory.GetComponentInChildren<Button>().GetComponentInChildren<Image>().enabled = true;
        victory.GetComponentInChildren<Button>().enabled = true;
        victory.enabled = true;

    }
}
