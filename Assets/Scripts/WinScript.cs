using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {
    SavedValues sv;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        sv = FindObjectOfType<SavedValues>();
	}
	
	// Update is called once per frame
	void Update () {
        if (sv.KillCount == sv.KillLimit || sv.ActiveComputers() == 0)
        {
            sv.Paused = true;
            Time.timeScale = 0;
            FindObjectOfType<CanvasScript>().ShowVictory();
        }

    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
