using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    Text enemies;
    Text computers;
    Text objective;
    GameObject victory;
    GameObject pause;
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
        victory = GameObject.FindGameObjectWithTag("Victory");
        victory.SetActive(false);
        pause = GameObject.FindGameObjectWithTag("Pause");
        pause.SetActive(false);
        sv = GameObject.FindObjectOfType<SavedValues>();
    }
	
	// Update is called once per frame
	void Update () {
        enemies.text = "Kills remaining: " + (sv.KillLimit-sv.KillCount);
        computers.text = "Hacks remaining: " + sv.ActiveComputers();
        if (Input.GetKeyDown("escape"))
            HideShowPause();
    }

    public void ShowVictory() { 
        victory.SetActive(true);
    }

    public void HideShowPause()
    {
        if (pause.activeSelf)
        {
            Time.timeScale = 1;
            sv.Paused = false;
        }
        else { 
        sv.Paused = true;
        Time.timeScale = 0;
    }
        pause.SetActive(!pause.activeSelf);
    }
}
