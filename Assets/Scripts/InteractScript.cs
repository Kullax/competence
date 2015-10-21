using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {
    private bool done = false;

    public Material offmat;

    void OnTriggerStay(Collider collider)
    {
        if (done)
            return;
        done = true;
        if (collider.tag != "Player")
            return;
        Renderer rend = GetComponent<Renderer>();
        rend.material = offmat;
        Camera cam = GameObject.FindObjectOfType<Camera>();
        SoundScript script = cam.GetComponent<SoundScript>();
        script.PlayRecieving();

    }
}
