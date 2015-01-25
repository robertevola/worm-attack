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

	public GameObject WormDeathEffect;
	public bool Dieing;

	GameObject deathEffect;
	// Use this for initialization
	void Start () 
    {
        controlledCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(objectToFollow != null)
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
			if(Dieing)
			{
				if(deathEffect==null)deathEffect = Instantiate(WormDeathEffect, objectToFollow.position, objectToFollow.rotation)as GameObject;

				deathEffect.transform.position = objectToFollow.transform.position+new Vector3(0.0f,0.0f,-1.0f);
				deathEffect.transform.rotation = objectToFollow.transform.rotation;

			}
		}

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
