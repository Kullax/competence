using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

    public AudioClip aware;
    private AudioSource source;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAware()
    {
        source.PlayOneShot(aware);
    }
}
