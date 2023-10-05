using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlane : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            GameManager.Instance.IncrementStage();
        }
    }
}
