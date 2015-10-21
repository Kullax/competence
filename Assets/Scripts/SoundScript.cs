using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

    public AudioClip aware;
    public AudioClip recieving;
    public AudioClip victory;
    public AudioClip pickup;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAware()
    {
        source.PlayOneShot(aware);
    }

    public void PlayRecieving()
    {
        source.PlayOneShot(recieving);
    }

    public void PlayVictory()
    {
        source.PlayOneShot(victory);
    }

    public void PlayPickup()
    {
        source.PlayOneShot(pickup);
    }
}
