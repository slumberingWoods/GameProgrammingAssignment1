using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.ResetScore();
        }
    }
}
