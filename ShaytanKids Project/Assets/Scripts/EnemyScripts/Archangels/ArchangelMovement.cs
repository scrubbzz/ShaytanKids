using UnityEngine;

public class ArchangelMovement : MonoBehaviour
{
    public int movementSpeed;
    public GameObject Target;
    public float distanceToPlayer;
    public ArchangelAttackManager archangelAttackManager;

    public bool toggle;
    [SerializeField]
    private PlayerAttack playerAttack;
    public float timerLength;
    public float currentTimer;

    public Vector3[] flightPositions;
    public int index;

    public float changePosTimer;

    //public Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        toggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        //LocatePlayer();
        ToggleFlightTowardsPlayer();
         FlyTowardsPlayer(toggle);
      
        
            FlyRandomly(toggle);
        
       
    }

    public void FlyTowardsPlayer(bool Toggle)
    {
        if(Toggle == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            distanceToPlayer = Vector2.Distance(rb.position, Target.transform.position);
            if (distanceToPlayer > 20 && distanceToPlayer < 50)
            {
                rb.transform.position = Vector2.MoveTowards(rb.position, Target.transform.position, movementSpeed * Time.deltaTime);
                //Debug.Log(transform.position);
            }
        }

    }
    public void ToggleFlightTowardsPlayer()
    {
        
        if (playerAttack.attacking)
        {
            currentTimer = timerLength;
        }
        if(currentTimer > 0)
        {
            toggle = true;
            currentTimer -= Time.deltaTime;
        }
        else
        {
            toggle = false;
        }
    }
    public void FlyRandomly(bool Toggle)
    {
       if(Toggle == false)
        {
            changePosTimer -= Time.deltaTime;
            if (changePosTimer <= 0)
            {
                index = Random.Range(0, flightPositions.Length);
                changePosTimer = 4f;
            }
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.position = Vector2.MoveTowards(rb.position, flightPositions[index], movementSpeed * Time.deltaTime);
          //  Debug.Log(changePosTimer);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 20);    
            
    }


}

