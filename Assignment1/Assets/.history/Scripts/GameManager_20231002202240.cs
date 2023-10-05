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
    private static float levelScore = 0;
    public static float doubleJump = 0;
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
    public void IncrementScore(float points)
    { 
        score += points;
        scoreNum.text = score.ToString();
    }
    public void IncrementStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelScore = score;
    }
    public static void ResetScore() {
        score = levelScore;
    }
}
