using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            if(Input.GetAxis("Vertical")>0f)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
