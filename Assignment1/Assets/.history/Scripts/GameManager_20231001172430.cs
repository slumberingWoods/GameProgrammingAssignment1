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
    public Text scoreNum;
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
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(Instance);
    }
    public void IncrementScore(float score)
    { 
        Debug.Log(score);
        Debug.Log(scoreNum.text);
        score += score;
    }
    public void IncrementStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
