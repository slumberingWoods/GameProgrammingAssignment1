using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
// Write down your variables here
    private static GameManager instance;
    public static float score = 0;
    public static bool doubleJump = false;
    public static GameManager Instance {
        get {
            if(instance==null) {
                instance = new GameManager();
            }
 
            return instance;
        }
    }
    public void Update()
    {
        if(doubleJump) {
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(Instance);
    }

    public static void IncrementScore(float score)
    { 
        score += score;
    }
    public void IncrementStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
