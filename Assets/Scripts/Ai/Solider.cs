using UnityEngine;
using System.Collections;

public class Solider : MonoBehaviour 
{
    private Vector2 WANDERING_TIMER_RANGE = new Vector2(2.0f, 7.0f);
    private Vector2 FLEEING_TIMER_RANGE = new Vector2(1.5f, 4.0f);
    private Vector2 FIGHT_TIMER_RANGE = new Vector2(3f, 3f);
    private Vector2 CHASING_TIMER_RANGE = new Vector2(0.1f, 0.25f);
    private Vector2 DEFAULT_TIMER_RANGE = new Vector2(1.0f, 5.0f);

    private Vector2 FLEE_MOVE_SPEED = new Vector2(1.0f, 3.0f);
    private Vector2 FIGHT_MOVE_SPEED = new Vector2(0.0f, -1.0f);
    private const float CHANCE_OF_FIGHTING = 0.85f;

    public enum SoliderState { WANDERING, FLEEING, FIGHTING, CHASING}
    public SoliderState currentState = SoliderState.WANDERING;

    public Transform target;
    public Vector2 baseMoveSpeed;
    private Vector2 additionalMoveSpeed;

    public float fightRadius = 2.0f;
    public int damage = 1;
    private Vector3 targetDirection;

    private bool updateStateBehaviour = true;
    private float updateStateTick = 0.0f;

    public GameObject walkAnimator;
	public GameObject shootAnimator;
    public GameObject deathParticleEffect;

	public AudioSource gunSource;

    // Use this for initialization
    void Start()
    {
        ToggleAnimatons(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateStateBehaviour)
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
        switch (currentState)
        {
            case SoliderState.WANDERING:
                Wander();
                break;
            case SoliderState.FLEEING:
                Flee();
                break;
            case SoliderState.FIGHTING:
                Fight();
                break;
            case SoliderState.CHASING:
                Chase();
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(updateStateTick);
        GenerateStateTick();
        updateStateBehaviour = true;
    }

    public void ChangeState(SoliderState state)
    {
        currentState = state;
        switch (currentState)
        {
            case SoliderState.WANDERING:
                additionalMoveSpeed = Vector3.zero;
                ToggleAnimatons(true);
                break;
            case SoliderState.FLEEING:
                additionalMoveSpeed = FLEE_MOVE_SPEED;
                ToggleAnimatons(true);
                break;
            case SoliderState.FIGHTING:
                additionalMoveSpeed = FIGHT_MOVE_SPEED;
                ToggleAnimatons(false);
                break;
            case SoliderState.CHASING:
                additionalMoveSpeed = FLEE_MOVE_SPEED;
                ToggleAnimatons(true);
                break;
            default:
                break;
        }
    }

    private void ToggleAnimatons(bool isWalking)
    {
        walkAnimator.SetActive(isWalking);
		shootAnimator.SetActive(!isWalking);
    }

    private void Wander()
    {
        targetDirection = Random.insideUnitSphere;
        targetDirection.z = 0;
    }

    private void Flee()
    {
        if (target == null)
			target = GameObject.FindGameObjectWithTag("WormHead").transform;

        targetDirection = transform.position - target.position;
        targetDirection.z = 0;
    }
	//int count = 0;
    private void Fight()
    {
		if (target == null)
			target = GameObject.FindGameObjectWithTag("WormHead").transform;  

        targetDirection = target.position - transform.position;
        targetDirection.z = 0;

		gunSource.PlayOneShot (gunSource.clip);
        //Debug.DrawRay(transform.position, targetDirection * fightRadius, Color.red, 2.0f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetDirection, fightRadius,9);
        foreach (RaycastHit2D hit in hits)
        {
            string hitTag = hit.transform.tag;
            if(hitTag == "WormHead" || hitTag == "WormBody" || hitTag == "WormTail")
            {

                hit.transform.SendMessageUpwards("ApplyDamage", damage);
				break;
            }
        }
        
        if (Vector3.Distance(transform.position, target.position) >= fightRadius)
            ChangeState(SoliderState.CHASING);
    }

    private void Chase()
    {
		if (target == null)
			target = GameObject.FindGameObjectWithTag("WormHead").transform;

        targetDirection = target.position - transform.position;
        targetDirection.z = 0;

        if (Vector3.Distance(transform.position, target.position) <= fightRadius)
            ChangeState(SoliderState.FIGHTING);
    }

    //private void Frenzy()
    //{
    //    float currentAngle;
    //    Vector3 axis;
    //    transform.rotation.ToAngleAxis(out currentAngle, out axis);

    //    Vector3 temp = targetDirection; ;
    //    temp.x = Mathf.Cos(currentAngle * Mathf.PI / 180 + Mathf.Atan2(targetDirection.y, targetDirection.x));
    //    temp.y = Mathf.Sin(currentAngle * Mathf.PI / 180 + Mathf.Atan2(targetDirection.y, targetDirection.x));
    //    targetDirection = temp;
    //}

    private void GenerateStateTick()
    {
        switch (currentState)
        {
            case SoliderState.WANDERING:
                RandomizeStateTick(WANDERING_TIMER_RANGE.x, WANDERING_TIMER_RANGE.y);
                break;
            case SoliderState.FLEEING:
                RandomizeStateTick(FLEEING_TIMER_RANGE.x, FLEEING_TIMER_RANGE.y);
                break;
            case SoliderState.FIGHTING:
                RandomizeStateTick(FIGHT_TIMER_RANGE.x, FIGHT_TIMER_RANGE.y);
                break;
            case SoliderState.CHASING:
                RandomizeStateTick(CHASING_TIMER_RANGE.x, CHASING_TIMER_RANGE.y);
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
        target = worm;
        float chance = Random.Range(0.0f, 1.0f);
        if (chance <= CHANCE_OF_FIGHTING)
        {
            ChangeState(SoliderState.FIGHTING);
            return;
        }

        ChangeState(SoliderState.FLEEING);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag == "WormHead")
        {
            if (Vector3.Distance(c.transform.position, transform.position) <= 1.5f)
            {
                c.SendMessageUpwards("AddBodyChunk");
                deathParticleEffect.transform.position = this.transform.position;
                GameManager.IncreaseScore(500);
                Instantiate(deathParticleEffect);
                Destroy(gameObject);
            }
            else 
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
            else 
                WormSeen(c.transform.parent.FindChild("Head"));
        }

    }

}
