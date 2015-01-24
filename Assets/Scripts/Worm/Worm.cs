using UnityEngine;
using System.Collections;

public class Worm : MonoBehaviour 
{
    //Input
    public Joystick movementStick;
    public Button digButton;

    private WormPiece headPiece, tailPiece, ringPiece;
    
    private Vector2 moveDirection;
    public Vector2 baseMovementSpeed;
    public Vector2 currentVelocity;
    public bool isDigging;

    public int health;
    public bool isAlive = true;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateMoveDirection();
        UpdateDigState();

        currentVelocity.x = baseMovementSpeed.x * moveDirection.x;
        currentVelocity.y = baseMovementSpeed.y * moveDirection.y;
	}

    private void UpdateMoveDirection()
    {
        if(movementStick.InUse)
        { //Player is currently interacting with the stick
            moveDirection.x = movementStick.Axis.x;
            moveDirection.y = movementStick.Axis.y >= 0.0f ? movementStick.Axis.y + 1.0f : Mathf.Clamp(1.0f + movementStick.Axis.y, 0.1f, 1.0f);
        }
        else
        {
            moveDirection = Vector2.up; //Player moves forward at base speed when no movement input is given
        }
    }

    private void UpdateDigState()
    {
        if(!isDigging)
        {
            if (digButton.IsPressed)
            {
                isDigging = true;
                DigDown();
            }
        }
        else
        {
            if(!digButton.IsHeldDown)
            {
                isDigging = false;
                Surface();
            }
        }
        
    }

    private void DigDown()
    {

    }

    private void Surface()
    {

    }

    private void ApplyDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            isAlive = false;
        }
    }
}
