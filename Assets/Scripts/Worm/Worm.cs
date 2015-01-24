using UnityEngine;
using System.Collections;

public class Worm : MonoBehaviour 
{
    //Input
    public Joystick movementStick;
    public Button digButton;
	public GameObject WormBodyPiece;

    private WormPiece tailPiece;
	private WormHead headPiece;
    private Vector2 moveDirection;
    public Vector2 baseMovementSpeed;
    public Vector2 currentVelocity;
    public bool isDigging;

    public int health;
    public bool isAlive = true;

	// Use this for initialization
	void Start () 
    {
		headPiece = transform.FindChild("Head").gameObject.GetComponent<WormHead>();
		tailPiece = transform.FindChild("Tail").gameObject.GetComponent<WormPiece>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateMoveDirection();
        UpdateDigState();

        headPiece.Turn(baseMovementSpeed.x * moveDirection.x);
        headPiece.AdjustSpeed(baseMovementSpeed.y * moveDirection.y);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			AddBodyChunk();
		}

	}

    private void UpdateMoveDirection()
    {
        if(movementStick.InUse)
        { //Player is currently interacting with the stick
            moveDirection.x = movementStick.Axis.x;
            moveDirection.y = movementStick.Axis.y >= 0.0f ? movementStick.Axis.y + 1.0f : Mathf.Clamp(1.0f + movementStick.Axis.y, 0.5f, 1.0f);
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
	public void AddBodyChunk()
	{
		Vector3 p = tailPiece.previous.transform.position;
		p.z += 0.001f;
		GameObject b = Instantiate(WormBodyPiece, p, tailPiece.previous.transform.rotation) as GameObject;
		b.transform.parent = this.transform;
		WormPiece wp = b.GetComponent<WormPiece>();
		wp.previous = tailPiece.previous;
		wp.previous.next = wp;
		tailPiece.previous = wp;
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
