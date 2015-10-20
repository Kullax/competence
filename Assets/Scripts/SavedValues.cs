using UnityEngine;
using System.Collections;

public class SavedValues : MonoBehaviour {
    private Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = new Vector3 (0,0,0);
    }

    public void setPoint(Vector3 newPoint)
    {
        pos = newPoint;
    }

    public Vector3 getPoint()
    {
        return pos;
    }
}
