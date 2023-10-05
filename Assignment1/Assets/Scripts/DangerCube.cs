using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DangerCube : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector3 pointA;
    public Vector3 pointB;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.timeSinceLevelLoad * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time); 
    }
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            this.transform.position = pointA;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.ResetScore();
        }
    }
}
