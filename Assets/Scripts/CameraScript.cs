using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Transform target;
    public float sensitivityY = 1f;
    public float sensitivityX = 1f;
    private float maxX, minX, maxY, minY;
    SavedValues sv;
    private Vector3 Point;

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
        transform.localPosition = Point;
        Vector3 input = new Vector3(Input.GetAxis("Mouse X") * sensitivityX, Input.GetAxis("Mouse Y") * sensitivityY, 0);
        target.transform.localPosition += input;
        if (target.transform.localPosition.x < minX)
        {
            target.transform.localPosition = new Vector3(minX, target.transform.localPosition.y, target.transform.localPosition.z);
        }
        if (target.transform.localPosition.x > maxX)
        {
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
//        Adjust();
        transform.LookAt(target);
    }

    void Adjust()
    {
        Vector3 wantedPosition = transform.position;
        // check to see if there is anything behind the target
        RaycastHit hit;
        Vector3 back = transform.TransformDirection(-1 * Vector3.forward);
        Vector3 start = transform.position + Vector3.forward;
        // cast the bumper ray out from rear and check to see if there is anything behind
        if (Physics.Raycast(start, back, out hit, 2f))
        {
            Debug.DrawLine(start, hit.point);
            // clamp wanted position to hit position
            wantedPosition -= back;// * hit.distance;
        }
        transform.position = wantedPosition;
//        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

/*        Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);

        if (smoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }
        else
            transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);*/
    }
}
