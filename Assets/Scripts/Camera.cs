using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
    public Transform target;
    public float sensitivityY = 1f;
    public float sensitivityX = 1f;
    private float maxX, minX, maxY, minY;

    void Start()
    {
        maxX = 3;
        minX = -3;
        maxY = 3f;
        minY = -0.5f;
    }


    // Update is called once per frame
    void Update () {
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
        transform.LookAt(target);
    }
}
