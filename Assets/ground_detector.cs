using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_detector : MonoBehaviour
{
    public bool isGrounded= true;
    
    private void onCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded=true;
        }

    }
   private void onCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded=false;
        }
    }

}
