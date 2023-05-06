using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player_Movement : MonoBehaviour
{
    public float moveSpeed =5f;
    public float jumpPower =10f;
    public float damageAmount=50f;
    float tempScale;
    public ground_detector groundDetector;
    public Rigidbody2D rb;
    Animator animator;
    public float attackCooldown=1f;
    bool canAttack=true;
    public EnemyAi enemy;

    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        tempScale=transform.localScale.x;
        animator=GetComponent<Animator>();
    }
    
    private void Update()
    {
        jump();
        Animations();
        Attack();
    }

    private void FixedUpdate()
    {
        
        transform.Translate(Vector3.right* Input.GetAxis("Horizontal")* moveSpeed * Time.deltaTime);
        if( Input.GetAxis("Horizontal")<0)
        {
            transform.localScale= new Vector3(-tempScale, tempScale,tempScale);
        }
        else if(Input.GetAxis("Horizontal")>0)
        {
            transform.localScale= new Vector3(tempScale, tempScale,tempScale);
        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.tag=="Enemy")
        {
            enemy=collision.GetComponent<EnemyAi>();
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
     {
        if(collision.tag=="Enemy")
        {
            enemy=null;
        }
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackDelay());
            animator.SetTrigger("attack");
            if(enemy!=null)
            {
                enemy.TakeDamage(damageAmount);
            }
        }
    }

    private void jump()
    {
        if(Input.GetButtonDown("Jump") && groundDetector.isGrounded)
        {
            animator.SetTrigger("jump");
            rb.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
        }
    }
    void Animations()
    {
        if(Input.GetAxis("Horizontal")!=0)
        {
            animator.SetBool("run",true);
        }
        else if(Input.GetAxis("Horizontal")==0)
        {
            animator.SetBool("run",false);
        }
    }
    IEnumerator AttackDelay()
    {
        canAttack =false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack=true;

    }
}
