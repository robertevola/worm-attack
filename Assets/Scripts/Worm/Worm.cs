using UnityEngine;
using System.Collections;

public class Worm : MonoBehaviour 
{
    //Input
    public Joystick movementStick;
    public Button digButton;
	public GameObject WormBodyPiece;
	public GameObject HoleGO;

    private WormPiece tailPiece;
	private WormHead headPiece;
    private Vector2 moveDirection;
    public Vector2 baseMovementSpeed;
    public bool isDigging;

    public int startingHealth = 100;
	private int currentHealth;
    public bool isAlive = true;

	public bool wormyControlsEnabled = false;
	// Use this for initialization
	void Start () 
    {
		headPiece = transform.FindChild("Head").gameObject.GetComponent<WormHead>();
		tailPiece = transform.FindChild("Tail").gameObject.GetComponent<WormPiece>();

		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(isAlive)
		{
	        UpdateMoveDirection();
	        UpdateDigState();

			headPiece.Turn(baseMovementSpeed.x * moveDirection.x);
	        headPiece.AdjustSpeed(baseMovementSpeed.y * moveDirection.y);

			if(Input.GetKeyDown(KeyCode.Space))
			{
				AddBodyChunk();
			}
			if(Input.GetKeyDown(KeyCode.LeftControl))
			{
				ToggleDig();
			}
		}

	}
	public void ToggleDig(){
		if (isAlive) {
			if (isDigging) {
				isDigging = false;
				Surface ();
			} else {	
				isDigging = true;
				DigDown ();
			}
		}
	}
	public void KillWorm()
	{
		Camera.main.GetComponent<CameraController>().Dieing = true;
		isAlive = false;
		headPiece.KillSelf();
	}
	public void SetWormyState(bool state)
	{
		wormyControlsEnabled = state;
	}

    private void UpdateMoveDirection()
    {
		if(isAlive)
		if(movementStick.InUse)
		{
			if(wormyControlsEnabled)
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
			else
			{
				if(movementStick.InUse)
				{ //Player is currently interacting with the stick
					Vector2 globalMoveDir = headPiece.transform.InverseTransformDirection(movementStick.Axis.x, movementStick.Axis.y, 0.0f);
					float angleBetween = Vector2.Angle(new Vector2(headPiece.transform.up.x, headPiece.transform.up.y), movementStick.Axis);

					moveDirection.x = globalMoveDir.x;
					moveDirection.y = globalMoveDir.y >= 0.0f ? globalMoveDir.y + 1.0f : Mathf.Clamp(1.0f + globalMoveDir.y, 0.5f, 1.0f);
				}
			}
		}
		else
		{
				moveDirection = Vector2.up;
		}


//		if(wormyControlsEnabled)
//		{
//	        
//		}else
//		{
////			float angleBetween = Vector2.Angle(new Vector2(headPiece.transform.up.x, headPiece.transform.up.y), movementStick.Axis);
////
////			float dif = Vector2.Dot(movementStick.Axis, new Vector2(headPiece.transform.up.x, headPiece.transform.up.y)); 
////
////			//Debug.Log ("Dot " + dif);
////
////			//headPiece.transform.TransformDirection(new Vector3(movementStick.Axis.x, movementStick.Axis.y));
////			Vector3 turn = headPiece.transform.TransformDirection(new Vector3(movementStick.Axis.x, movementStick.Axis.y));
////
////			moveDirection.x = turn.x;
////			moveDirection.y = Vector2.Dot (new Vector2(headPiece.transform.up.x, headPiece.transform.up.y),movementStick.Axis);
//
//
//			}
//			else
//			{
//				moveDirection = new Vector2(transform.up.x, transform.up.y); //Player moves forward at base speed when no movement input is given
//			}
//
//
//		}


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
		((WormPiece)wp).previous.next = wp;
		tailPiece.previous = wp;
		wp.next = tailPiece;
	}

    private void DigDown()
    {
		GameObject go = Instantiate(HoleGO, headPiece.transform.position, Quaternion.identity) as GameObject;
		headPiece.isUnderground = true;
		HoleScript HS = go.GetComponent<HoleScript>();
		HS.next = headPiece;
		headPiece.next.previous = HS;
		HS.Entry = true;
		//Animate worm
    }

    private void Surface()
    {
		GameObject go = Instantiate(HoleGO, headPiece.transform.position, Quaternion.identity) as GameObject;
		headPiece.isUnderground = false;
		HoleScript HS = go.GetComponent<HoleScript>();
		HS.next = headPiece;
		headPiece.next.previous = HS;
		HS.Entry = false;
		//Animate worm
    }

    public void ApplyDamage(int damage)
    {

        currentHealth -= damage;
		if(currentHealth <= 0)
        {
			currentHealth = 0;
            isAlive = false;
        }
		Debug.Log("currentHealth:" + currentHealth);
    }

	public void ApplyHeal(int heal)
	{		currentHealth += heal;

		if(currentHealth >= startingHealth)
		{
			currentHealth = startingHealth;
		}
	}
}
