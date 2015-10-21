using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

    public Material offmat;

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        Renderer rend = GetComponent<Renderer>();
        rend.material = offmat;

    }
}
