using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset_level : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.transform.tag=="Player")
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
