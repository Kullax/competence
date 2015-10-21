using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Transform target;
    public Transform player;
    public float sensitivityY = 1f;
    public float sensitivityX = 1f;
    private float maxX, minX, maxY, minY;
    public LayerMask mask = -1;

    SavedValues sv;
    private Vector3 Point;


    public float minDistance = 1.0f;
    public float maxDistance = 2.0f;
    public float smooth = 0.10f;
    Vector3 dollyDir;
    float distance;

    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void Start()
    {
        Point = transform.localPosition;
        sv = FindObjectOfType<SavedValues>();
        maxX = 3;
        minX = -3;
        maxY = 3f;
        minY = -0.5f;
    }


    // Update is called once per frame
    void Update () {
        if (sv.Paused)
            return;
//        transform.localPosition = Point;
        Vector3 input = new Vector3(Input.GetAxis("Mouse X") * sensitivityX, Input.GetAxis("Mouse Y") * sensitivityY, 0);
        target.transform.localPosition += input;
        if (target.transform.localPosition.x < minX)
        {
            GameObject.FindGameObjectWithTag("Player").transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * 150f * Time.deltaTime);
            target.transform.localPosition = new Vector3(minX, target.transform.localPosition.y, target.transform.localPosition.z);
        }
        if (target.transform.localPosition.x > maxX)
        {
            GameObject.FindGameObjectWithTag("Player").transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * 150f * Time.deltaTime);
            target.transform.localPosition = new Vector3(maxX, target.transform.localPosition.y, target.transform.localPosition.z);
        }
        if (target.transform.localPosition.y < minY)
        {
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, minY, target.transform.localPosition.z);
        }
        if (target.transform.localPosition.y > maxY)
        {
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, maxY, target.transform.localPosition.z);
        }
        Adjust2();
        transform.LookAt(target);
    }

    void Adjust2()
    {
        Vector3 desiredCameraPos = target.transform.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        Vector3 back = player.TransformDirection(Vector3.back);
        Vector3 start = player.position;

        if (Physics.Raycast(start, back, out hit, minDistance, mask))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition= Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
