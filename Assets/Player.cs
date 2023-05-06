using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public RectTransform healthbar;
    public float health;
    public float maxHealth = 100f;
    Animator animator;

     private void Start() 
        {
            health = maxHealth;
            animator=GetComponent<Animator>();
        }

         private void Update() 
        {
            healthbar.localScale=new Vector3(health/ 100f,1f,1f);

        }

         private void LateUpdate()
        {
            health=Mathf.Clamp(health,0f,maxHealth);
        }

        public void TakeDamage(float amount)
        {
            if(amount >= health)
            {
                health = 0f;
                //die
                animator.SetTrigger("death");
            }
            else if(amount<health)
            {
                health -= amount;
            }
        }
}
