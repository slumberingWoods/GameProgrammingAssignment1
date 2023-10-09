using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollider : MonoBehaviour
{
    public float speed = 0.5f;
    public bool isMoving;
    public Vector3 pointA;
    public Vector3 pointB;
    public GameObject PointsParticle;
    // Update is called once per frame
    void Update()
    {
        if(isMoving) {
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(pointA, pointB, time); 
        }
        transform.Rotate(new Vector3(0,Time.time * 50,0));
    }
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            GameObject firework = Instantiate(PointsParticle, this.transform.position, Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
            GameManager.Instance.IncrementScore(50);
        }
    }
}
