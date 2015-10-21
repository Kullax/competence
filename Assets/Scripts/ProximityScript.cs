using UnityEngine;

public class ProximityScript : MonoBehaviour {

    private Vector3 pos;
    private float t;
    private SavedValues sv;

	// Use this for initialization
	void Start () {
        sv = FindObjectOfType<SavedValues>();
    }

    // Update is called once per frame
    void Update () {
        float l = 21;
        foreach (var enemy in sv.getEnemies())
        {
            pos = enemy.transform.position;
            float tmp = sv.CalculatePathLength(sv.PlayerNav, pos);
            if (tmp <= l)
            {
                l = tmp;
            }
        }
        if (l > 20)
            SetWhite();
        else
            SetRed();
	}

    void SetRed()
    {
        float l = t / 2f;
        RenderSettings.fogDensity = Mathf.Lerp(0.0f, 0.05f, l);
        RenderSettings.fog = true;
        if(t < 2f)
            t += Time.deltaTime;
    }

    void SetWhite()
    {
        float l = t / 2f;
        RenderSettings.fogDensity = Mathf.Lerp(0.0f, 0.05f, l);
        RenderSettings.fog = true;
        if(t > 0)
            t -= Time.deltaTime;
    }



}
