using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {
    public bool done = false;

    public Material offmat;
    SavedValues sv;

    void Start()
    {
        sv = FindObjectOfType<SavedValues>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        if (done)
            return;
        done = true;
        Renderer rend = GetComponent<Renderer>();
        rend.material = offmat;
        Camera cam = GameObject.FindObjectOfType<Camera>();
        SoundScript script = cam.GetComponent<SoundScript>();
        if (sv.ActiveComputers() == 0)
            script.PlayVictory();
        else
            script.PlayRecieving();

    }
}
