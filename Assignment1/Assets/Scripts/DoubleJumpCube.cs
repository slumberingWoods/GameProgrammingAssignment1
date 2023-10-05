using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCube : MonoBehaviour
{
    float duration = 15.0f;
    float recreateDuration = 20.0f;
    public GameObject DoubleJumpParticle;
    public void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("Triggered by Players");
            GameManager.Instance.DoubleJump();
            GameObject firework = Instantiate(DoubleJumpParticle, this.transform.position, Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
            if(!IsInvoking(nameof(ResetJump))) {
                Debug.Log("Reset invoke");
                CancelInvoke(nameof(ResetJump));
            }
            Invoke(nameof(ResetJump), duration);
            Invoke(nameof(Recreate), recreateDuration);
        }
    }
    public void ResetJump() {
        GameManager.Instance.ResetJump();
    }
    public void Recreate() {
        gameObject.SetActive(true);        
    }
}
