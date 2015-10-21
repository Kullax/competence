using UnityEngine;

public class XRayMouseOver : MonoBehaviour {

    private Renderer _renderer;
    private SavedValues sv;
    public LayerMask mask = -1;
    GameObject arm;
    GameObject empty;


    // Use this for initialization
    void Start () {
	    _renderer = GetComponent<Renderer>();
        sv = FindObjectOfType<SavedValues>();

	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!sv.ArmLooted)
        {
            arm = GameObject.FindGameObjectWithTag("LootArm");
            empty = GameObject.FindGameObjectWithTag("LootEmpty");
        }
        else
        {
            arm = GameObject.FindGameObjectWithTag("Arm");
            empty = GameObject.FindGameObjectWithTag("Empty");
        }
        var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;
	    if (Physics.Raycast(arm.transform.position, empty.transform.position-arm.transform.position, out hit, Mathf.Infinity, mask))
        { 
            Debug.DrawRay(arm.transform.position, empty.transform.position - arm.transform.position);
            if (hit.collider.gameObject == gameObject)
            {
                _renderer.material.SetVector("_Center", hit.point);
                sv.setPoint(hit.point);

            }
            else
            {

                _renderer.material.SetVector("_Center", sv.getPoint());
            }
        }
        else {
            // Somewhere below the level
	        _renderer.material.SetVector("_Center", new Vector3(0,-10,0));            
	    }

	}
}