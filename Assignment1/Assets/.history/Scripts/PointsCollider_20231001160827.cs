using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollider : MonoBehaviour
{
        public float speed = 0.5f;
    public Vector3 pointA;
    public Vector3 pointB;
    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time); 
    }
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            gameObject.SetActive(false);
            GameManager.IncrementScore(50);
        }
    }
}
