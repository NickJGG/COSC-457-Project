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
        ////Debug.Log("Player has been selected as Target");
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) < detectionRange)
        {
            ////Debug.Log("Enemy has been detected");
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            m_animator.SetInteger("AnimState", 2);
            enemyDetected = true;
        }
        else
        {
            //////Debug.Log("Enemy is not detected");
            m_animator.SetInteger("AnimState", 0);
        }

        if (target.position.x > transform.position.x && Mathf.Abs(Vector2.Distance(target.position, transform.position)) < detectionRange)
        {

            transform.localScale = new Vector3(-scale, scale, scale);

            
            //m_animator.SetInteger("AnimState", 13);
        }
        else
        {
            transform.localScale = new Vector3(scale, scale, scale);

        }
        

        
        if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) < attackRange)
        {

            ////Debug.Log("Enemy is within attack range");
            m_animator.SetInteger("AnimState", 0);
            //m_animator.SetInteger("AnimState", 0);
            //m_animator.SetTrigger("Attack");
            //m_animator.SetInteger("AnimState", 101);

            if (Time.time > next_attacktime + cooldowntime)
            {

                ////Debug.Log("Begining Attack Animation");
                m_animator.SetInteger("AnimState", 101);


                //Damage to player needs to occure here
                //Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


                /*
                Collider2D[] hitenemy = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);


                foreach (Collider2D enemy in hitenemy)
                {

                    //Debug.Log("Begining Attack Animation");

                    //Deal Damage to enemy here
                    if (enemy.GetComponent<HeroKnight>().isBlocking)
                    {

                        //Debug.Log("Player Blocked the Attack");

                    }
                    else
                    {

                        //Debug.Log("Damage has been dealt to player");
                        enemy.GetComponent<PlayerStats>().TakeDamage(1);
                    }

                    
                }

                */

                HeroKnight player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();

                if (player.isBlocking)
                {
                    player.m_animator.SetBool("BlockHit", true);

                    player.isBlocking = false;

                    StartCoroutine(BlockHit());
                    //player.m_animator.SetTrigger("BlockHit");

                    player.m_animator.SetBool("BlockHit", false);
                }
                else
                {

                    ////Debug.Log("Damage has been dealt to player");
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(1);
                }
                next_attacktime = Time.time;

            }



        }
        else
        {

            ////Debug.Log("Enemy is NOT within attack range");
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //m_animator.SetTrigger("Death");


        /*
        Collider2D[] detectenemy = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRange, enemyLayers);
        
        foreach (Collider2D enemy in detectenemy)
        {
            ////Debug.Log("Hit on " + enemy.name);
            //m_animator.SetTrigger("Attack");
            //m_animator.SetTrigger();
            
            //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            enemyDetected = true;

        }
        
        */
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        /*
        if (enemyDetected)
        {

            m_animator.SetInteger("AnimState", 2);
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


        }
        else
        {

            m_animator.SetInteger("AnimState", 0);
        }

        */





        /*
        
        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
      
            foreach (Collider2D enemy in hitenemy)
            {
                enemyDetected = false;
                m_animator.SetInteger("AnimState", 0);
                //Debug.Log("Hit on " + enemy.name);
                m_animator.SetTrigger("Attack");
                //m_animator.SetTrigger();
                //enemyDetected = true;
            }

        */
    }

    IEnumerator BlockHit() {
        yield return new WaitForSeconds(.75f);
    }
}

