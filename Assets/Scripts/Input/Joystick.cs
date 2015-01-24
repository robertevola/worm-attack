using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
public class Joystick : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform joystickTransform;
    private Vector2 originAnchorPosition; //Origin Position of the Stick
    private Vector2 targetAnchorPosition; //Target position Stick will lerp to
     
    public float movementSensitivity = 2.0f; //Sensitivity of the lerp speed of the stick
    public float movementRadius; //Radius in which stick will move within
    private float movementBuffer; //Buffer of space player can move their thumb out of the radius before stick resets
    private Vector2 movementPosition; //Position of the players thumb in the canvas
    private Vector2 movementDirection; //Direction from the origin to the players thumb

    private bool joystickInUse = false;
    public bool InUse
    {
        get { return joystickInUse; }
    }
    public Vector2 Axis
    {
        get
        {
            return movementDirection.normalized;
        }
    }

	// Use this for initialization
	void Start () 
    {
        joystickTransform = GetComponent<RectTransform>();
        //joystickTransform.pivot = new Vector2(0.5f, 0.5f); //Ensure pivot is in center of joystick
        originAnchorPosition = joystickTransform.anchoredPosition; //Save the originPosition of the joystick from it's anchor
        targetAnchorPosition = originAnchorPosition; //Target starts of as origin

        movementBuffer = joystickTransform.rect.width / 2.0f; //Buffer is set to half the width of the stick graphic (assuming width == height)
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Check if position should be updated, or stick reset only if in use
        if(joystickInUse) 
        {
            float distanceFromOrigin = movementDirection.magnitude;
            float maxDistanceAllowed = movementRadius + movementBuffer;

            if (distanceFromOrigin <= maxDistanceAllowed)
            {
                float distanceToSet = (distanceFromOrigin > movementRadius) ? movementRadius : distanceFromOrigin;
                targetAnchorPosition = originAnchorPosition + movementDirection.normalized * distanceToSet;
            }
            else
                ResetJoystick();
        }

        SetAnchorPosition(targetAnchorPosition);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log(name + " Engaged ...");
        joystickInUse = true;
    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log(name + " Being Dragged");

        if (!joystickInUse)
        {
            data.currentInputModule.DeactivateModule();
            return;
        }
     
        //Set the movePosition to the position where the user has their thumb
        //movementPosition.x = data.position.x;
        //movementPosition.y = data.position.y;
        movementPosition = data.position;

        movementDirection = movementPosition - originAnchorPosition; //Direction pointing from the joystick origin to the players thumb
    }

    public void OnEndDrag(PointerEventData data)
    {
        //Debug.Log(name + " Released ...");
        ResetJoystick();
    }

    private void ResetJoystick()
    {
        //Debug.Log("Joystick Being Reset ...");
        joystickInUse = false;
        targetAnchorPosition = originAnchorPosition;
        movementDirection = Vector2.zero;
    }

    private void SetAnchorPosition(Vector2 pos)
    {
        joystickTransform.anchoredPosition = Vector2.Lerp(joystickTransform.anchoredPosition, targetAnchorPosition, Time.deltaTime * movementSensitivity);
    }
}
