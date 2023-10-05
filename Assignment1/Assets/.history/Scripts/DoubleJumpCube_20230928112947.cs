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
            duration += 5;
            Invoke(nameof(ResetEffect), duration);
        }
    }

    public void ResetEffect() {
        Debug.Log("STOP JUMPING");
        GameManager.doubleJump = false;
    }
}
