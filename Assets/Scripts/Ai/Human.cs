using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour 
{
    private Vector2 WANDERING_TIMER_RANGE = new Vector2(2.0f, 7.0f);
    private Vector2 FLEEING_TIMER_RANGE = new Vector2(1.5f, 4.0f);
    private Vector2 FRENZY_TIMER_RANGE = new Vector2(0.2f, 0.5f);
    private Vector2 DEFAULT_TIMER_RANGE = new Vector2(1.0f, 5.0f);

    private const float CHANCE_OF_FRENZY = 0.25f;

    private Vector2 FLEE_MOVE_SPEED = new Vector2(1.0f, 3.0f);
    public enum HumanState { WANDERING, FLEEING, FRENZY }
    public HumanState currentState = HumanState.FRENZY;

    public Transform runAwayFrom;
    public Vector2 baseMoveSpeed;
    private Vector2 additionalMoveSpeed;

    private Vector3 targetPosition;
    public Vector3 targetDirection = new Vector3(1, 0, 0);

    private bool updateStateBehaviour = true;
    private float updateStateTick = 0.0f;

    private WormHeadAnimation basicAnimator;
    public GameObject deathParticleEffect;

	// Use this for initialization
	void Start () 
    {
        basicAnimator = transform.FindChild("HumanSprite").GetComponent<WormHeadAnimation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(updateStateBehaviour)
        {
            StartCoroutine(UpdateCurrentState());
        }

        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), (baseMoveSpeed.x + additionalMoveSpeed.x) * Time.deltaTime);
        transform.Translate(transform.up * (baseMoveSpeed.y + additionalMoveSpeed.y) * Time.deltaTime, Space.World);
	}

    private IEnumerator UpdateCurrentState()
    {
        updateStateBehaviour = false;
        switch(currentState)
        {
            case HumanState.WANDERING:
                Wander();
                break;
            case HumanState.FLEEING:
                Flee();
                break;
            case HumanState.FRENZY:
                Frenzy();
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(updateStateTick);
        GenerateStateTick();
        updateStateBehaviour = true;
    }

    public void ChangeState(HumanState state)
    {
        currentState = state;
        switch (currentState)
        {
            case HumanState.WANDERING:
                additionalMoveSpeed = Vector3.zero;
                basicAnimator.FramesPerSec = 6;
                break;
            case HumanState.FLEEING:
                additionalMoveSpeed = FLEE_MOVE_SPEED;
                basicAnimator.FramesPerSec = 8;
                break;
            case HumanState.FRENZY:
                additionalMoveSpeed = FLEE_MOVE_SPEED;
                basicAnimator.FramesPerSec = 8;
                break;
            default:
                break;
        }
        updateStateBehaviour = true;
    }

    private void Wander()
    {
        targetDirection = Random.insideUnitSphere;
        targetDirection.z = 0;
    }

    private void Flee()
    {
        if (runAwayFrom == null)
            runAwayFrom = transform;

        targetDirection = transform.position - runAwayFrom.position;
        targetDirection.z = 0;
    }

    private void Frenzy()
    {
        float currentAngle;
        Vector3 axis;
        transform.rotation.ToAngleAxis(out currentAngle, out axis);

        Vector3 temp = targetDirection;;
        temp.x = Mathf.Cos(currentAngle * Mathf.PI / 180 + Mathf.Atan2(targetDirection.y, targetDirection.x));
        temp.y = Mathf.Sin(currentAngle * Mathf.PI / 180 + Mathf.Atan2(targetDirection.y, targetDirection.x));
        targetDirection = temp;
    }

    private void GenerateStateTick()
    {
        switch (currentState)
        {
            case HumanState.WANDERING:
                RandomizeStateTick(WANDERING_TIMER_RANGE.x, WANDERING_TIMER_RANGE.y);
                break;
            case HumanState.FLEEING:
                RandomizeStateTick(FLEEING_TIMER_RANGE.x, FLEEING_TIMER_RANGE.y);
                break;
            case HumanState.FRENZY:
                RandomizeStateTick(FRENZY_TIMER_RANGE.x, FRENZY_TIMER_RANGE.y);
                break;
            default:
                RandomizeStateTick(DEFAULT_TIMER_RANGE.x, DEFAULT_TIMER_RANGE.y);
                break;
        }
    }

    private void RandomizeStateTick(float min, float max)
    {
        updateStateTick = Random.Range(min, max);
    }

    public void WormSeen(Transform worm)
    {
        runAwayFrom = worm;
        float chance = Random.Range(0.0f, 1.0f);
        if (chance <= CHANCE_OF_FRENZY)
        {
            ChangeState(HumanState.FRENZY);
            return;
        }

        ChangeState(HumanState.FLEEING);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag == "WormHead")
        {
            if(Vector3.Distance(c.transform.position, transform.position) <= 1.5f)
            {
                c.SendMessageUpwards("AddBodyChunk");
                deathParticleEffect.transform.position = this.transform.position;
                GameManager.IncreaseScore(200);
                Instantiate(deathParticleEffect);
                Destroy(gameObject);
            }
            else if (currentState == HumanState.WANDERING)
                WormSeen(c.transform);
        }
        else if (c.tag == "WormBody")
        {
            if (Vector3.Distance(c.transform.position, transform.position) <= 1.5f)
            {
                deathParticleEffect.transform.position = this.transform.position;
                Instantiate(deathParticleEffect);
                Destroy(gameObject);
            }
            else if (currentState == HumanState.WANDERING)
                WormSeen(c.transform.Find("Head"));
        }
      
    }

}
