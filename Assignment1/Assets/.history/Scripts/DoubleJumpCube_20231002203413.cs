using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCube : MonoBehaviour
{
    float duration = 5;
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            GameManager.doubleJump = true;
            gameObject.SetActive(false);

        }
    }
    public void ResetJump() {
        GameManager.ResetJump();
    }
}
