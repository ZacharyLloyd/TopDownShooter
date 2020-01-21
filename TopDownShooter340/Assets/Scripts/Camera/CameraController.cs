using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires a camera component
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    public float targetRotateSpeed;
    private Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMousePosition();
    }

    void RotateToMousePosition()
    {
        // Define the plane that the target is on
        Plane groundPlane;
        groundPlane = new Plane(Vector3.up, target.position);

        // Find the distance down the ray that the plane intersection is at
        float distance;
        Ray theRay = theCamera.ScreenPointToRay(Input.mousePosition);

        /* For future reference if I need to write it out or more access to pieces
        Same as above, but written out
        theRay = new Ray();
        theRay.origin = theCamera.ScreenToWorldPoint(Input.mousePosition);
        theRay.direction = theCamera.transform.forward;
        */

        if (groundPlane.Raycast(theRay, out distance))
        {
            // Find world point where intersection is
            Vector3 intersectionPoint = theRay.GetPoint(distance);

            // Rotate Towards that Point
            Quaternion targetRotation;
            Vector3 lookVector = intersectionPoint - target.position;
            targetRotation = Quaternion.LookRotation(lookVector, Vector3.up);
            target.rotation = Quaternion.RotateTowards(target.rotation, targetRotation, targetRotateSpeed * Time.deltaTime);

        }
    }
}
