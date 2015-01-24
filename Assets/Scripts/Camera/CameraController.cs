using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private Camera controlledCamera;

    public Transform objectToFollow;
    public float distanceFromTarget = 10.0f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public float translatingSmoothFactor = 5.0f;
    public float rotatingSmoothFactor = 10.0f;

    public bool rotateWithTarget = true;

	// Use this for initialization
	void Start () 
    {
        controlledCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
        targetPosition = objectToFollow.position + (-objectToFollow.forward * distanceFromTarget);
        Vector3 tempPos = Vector3.Lerp(controlledCamera.transform.position, targetPosition, Time.deltaTime * translatingSmoothFactor);
        controlledCamera.transform.position = tempPos;

	    if(rotateWithTarget)
        {
            targetRotation = objectToFollow.rotation;
        }

        Quaternion tempRot = Quaternion.Slerp(controlledCamera.transform.rotation, targetRotation, Time.deltaTime * rotatingSmoothFactor);
        controlledCamera.transform.rotation = tempRot;
	}

    public void SetRotateWithTarget(bool rotate)
    {
        rotateWithTarget = rotate;
        if(rotateWithTarget == false)
        {
            targetRotation = Quaternion.identity;
        }
    }
}
