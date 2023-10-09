using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollider : MonoBehaviour
{
    float points = 50;
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            gameObject.SetActive(false);
            Invoke(nameof(IncrementScore), score);
        }
    }
}
