using UnityEngine;

public class XRayMouseOver : MonoBehaviour {

    private Renderer _renderer;

	// Use this for initialization
	void Start () {
	    _renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;
	    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
	        _renderer.material.SetVector("_Center", hit.point);
	    else {
	        _renderer.material.SetVector("_Center", float.MaxValue * Vector4.one);
	    }
	}
}