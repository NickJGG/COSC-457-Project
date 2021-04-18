using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovementAI : MonoBehaviour
{


    public Animator m_animator;//Animator for enemy
    public bool hasDetected; //has Detected the enemy
    public LayerMask enemyLayers; // For landing hits

    private Transform target;
    public float speed;
    public Transform detectionPoint;
    public float detectionRange = 5f;
    public bool walk = true;


    public Transform attackPoint;
    public float attackRange;
    public float attackDamage = 1;
    private bool enemyDetected;

    public bool isLeftFacing = true;
    public int scale;


    public float cooldowntime;
    private float next_attacktime;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //Get Location of Player
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) < detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            m_animator.SetInteger("AnimState", 2);
            enemyDetected = true;
        }
        else
            m_animator.SetInteger("AnimState", 0);

        if (target.position.x > transform.position.x && Mathf.Abs(Vector2.Distance(target.position, transform.position)) < detectionRange)
        
            transform.localScale = new Vector3(-scale, scale, scale);
        else
            transform.localScale = new Vector3(scale, scale, scale);
        
        if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) < attackRange)
        {
            m_animator.SetInteger("AnimState", 0);

            if (Time.time > next_attacktime + cooldowntime)
            {
                m_animator.SetInteger("AnimState", 101);

                HeroKnight player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();

                if (player.isBlocking)
                {
                    player.m_animator.SetBool("BlockHit", true);

                    player.isBlocking = false;

                    StartCoroutine(BlockHit());

                    player.m_animator.SetBool("BlockHit", false);
                }
                else
                {
                    StartCoroutine(AttackPlayer());
                }

                next_attacktime = Time.time;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BlockHit() {
        yield return new WaitForSeconds(.75f);
    }
    IEnumerator AttackPlayer() {
        yield return new WaitForSeconds(.5f);

        HeroKnight player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();

        if (!player.isBlocking && Mathf.Abs(Vector2.Distance(target.position, transform.position)) < attackRange)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(attackDamage);
    }
}

