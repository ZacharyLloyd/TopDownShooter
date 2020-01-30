using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires a camera component
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Camera Variables")]
    [SerializeField]
    Transform target;
    [SerializeField, Range(0f, 500f), Tooltip("The speed at which the camera will rotate the player")]
    float targetRotateSpeed;
    private Camera theCamera;
    [SerializeField, Tooltip("The offset for the camera coming off the player")]
    public Vector3 offset;
    readonly private Space offsetPositionSpace = Space.Self;
    readonly private bool lookAt = true;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        target = FindObjectOfType<PlayerPawn>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMousePosition();
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            Refresh();
        }
    }
    /// <summary>
    /// This function is use to have the camera rotate towards where the mouse is pointing
    /// on a plane created in memory. It that takes that point and starts rotating the camera
    /// to that point at the speed set by targetRotateSpeed in the editor
    /// </summary>
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
    /// <summary>
    /// This function is basically an update function that will always make sure that
    /// the camera is rotating towards where the mouse is on the plane and that the camera is
    /// always following the target.
    /// </summary>
    public void Refresh()
    {
        //Checks to make sure the target exists
        //along with making sure the camera is at the offset position
        if (offsetPositionSpace == Space.Self)
        {
            if (target != null)
            {
                transform.position = target.TransformPoint(offset);
            }
        }
        //Add the offset to the camera's position
        else
        {
            transform.position = target.position + offset;
        }
        //Set the transform's rotation of the camera to the same as the player
        if (lookAt)
        {

            if (target != null)
            {
                transform.LookAt(target);
            }
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
