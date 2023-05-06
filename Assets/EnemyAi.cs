using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    public float detectionRange=20f;
    public float attackDelay=1f;
    public float attackRange=2f;
    public float attackAmount=10f;
    public float health;
    public float maxHealth=100f;

    AIPath aiPath;
    AIDestinationSetter setter;
    Rigidbody2D rb;
    Transform player;
    Animator animator;
    Seeker seeker;
    SpriteRenderer gfx;
    Player playerStats;

    bool canAttack=true;
    bool isAttacking =false;
    bool isDead=false;

    void Start()
    {
        setter=GetComponent<AIDestinationSetter>();
        player =GameObject.FindGameObjectWithTag("Player").transform;
        animator= GetComponent<Animator>();
        gfx=GetComponent<SpriteRenderer>();
        aiPath= GetComponent<AIPath>();
        playerStats=player.GetComponent<Player>();
        seeker=GetComponent<Seeker>();
        rb=GetComponent<Rigidbody2D>();
        health=maxHealth;

    }
    void Update()
    {
        if(!isDead)
        {
            if(aiPath.desiredVelocity.x<0.01f)
        {
            gfx.flipX=false;
        }
        else if(aiPath.desiredVelocity.x>0.01f)
        {
            gfx.flipX=true;
        }
        if(!isAttacking)
        {
            WalkManager();
        }
        
        AttackManager();
        }
    }
   private void LateUpdate()
    {
        health=Mathf.Clamp(health,0f,maxHealth);
    }
    void AttackManager()
    {
        if(Vector2.Distance(player.position,transform.position)<attackRange)
        {
            if(canAttack)
            {
                isAttacking=true;
                StartCoroutine(AttackDelay());
            } 
            else if(Vector2.Distance(player.position,transform.position)>attackRange)
            {
                 StopCoroutine(AttackDelay());
                 isAttacking=false;
            }
        }
    }
    void WalkManager()
    {
        
        if(Vector2.Distance(player.position,transform.position)<detectionRange)
        {
            setter.target=player;
            animator.SetBool("Walk",true);
        }
         else if(Vector2.Distance(player.position,transform.position)>detectionRange)
        {
            setter.target=null;
             animator.SetBool("Walk",false);
        }
    }
    IEnumerator AttackDelay()
    {
        canAttack =false;
        animator.SetTrigger("Attack");
        playerStats.TakeDamage(attackAmount);
        yield return new WaitForSeconds(attackDelay);
        canAttack=true;
    }
    public void  TakeDamage(float amount)
    {
        if(health <= amount)
        {
            health = 0f;
            //die;
            animator.SetTrigger("Death");
            seeker.enabled=false;
            aiPath.enabled=false;
            setter.enabled=false;
            Destroy(rb);
            isDead = true;
        }
        else if (health>amount)
        {
            health-=amount;
        }
    }

}
