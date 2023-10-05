using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCube : MonoBehaviour
{
    float duration = 5;
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            GameManager.doubleJump += 1;
            gameObject.SetActive(false);
            Invoke(nameof(ResetEffect), duration);
        }
    }
    public void ResetEffect() {
        GameManager.doubleJump -= 1;
    }
}
