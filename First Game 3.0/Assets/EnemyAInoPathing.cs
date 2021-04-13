using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAInoPathing : MonoBehaviour
{


    public Animator m_animator;//Animator for enemy
    public bool hasDetected; //has Detected the enemy
    public LayerMask enemyLayers; // For landing hits

    private Transform target; 
    public float speed;
    public Transform detectionPoint;
    public float detectionRange = 5f;


    public Transform attackPoint;
    public float attackRange = 2f;
    private bool enemyDetected;

    public bool isLeftFacing = true;
    public int scale;

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
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //m_animator.SetInteger("AnimState", 2);
            enemyDetected = true;
        }
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }

        if (target.position.x > transform.position.x && Mathf.Abs(Vector2.Distance(target.position, transform.position)) < detectionRange)
        {
            transform.localScale = new Vector3(-scale, scale, scale);

            m_animator.SetTrigger("Attack");

            m_animator.SetInteger("AnimState", 13);
        }
        else
        {
            transform.localScale = new Vector3(scale, scale, scale);

        }



        if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) < attackRange)
        {
            Debug.Log("Hit on player");
            //m_animator.SetInteger("AnimState", 0);
            m_animator.SetTrigger("Attack");
        }
        else
        {
            
        }
































        //m_animator.SetTrigger("Death");


        /*
        Collider2D[] detectenemy = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRange, enemyLayers);
        
        foreach (Collider2D enemy in detectenemy)
        {
            //Debug.Log("Hit on " + enemy.name);
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
                Debug.Log("Hit on " + enemy.name);
                m_animator.SetTrigger("Attack");
                //m_animator.SetTrigger();
                //enemyDetected = true;
            }

        */




    }






}
