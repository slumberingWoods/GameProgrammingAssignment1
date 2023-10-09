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
    public bool doubleJump = false;
    public Text doubleJumpText;
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
        if(scoreNum != null) {
            scoreNum.text = score.ToString();
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(Instance);
    }
    public void IncrementScore(float points)
    { 
        score += points;
    }
    public void IncrementStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelScore = score;
    }
    public void DoubleJump() {
        doubleJump = true;
        doubleJumpText.text = "Double Jump Active";
    }
    public void ResetJump() {
        doubleJump = false;
        doubleJumpText.text = "";
    }
    public static void ResetScore() {
        score = levelScore;
    }
    public static void ReturnToStart() {
        score = 0;
        SceneManager.LoadScene(0);
    }
    public static void EndGame() {
        Application.Quit();
    }
}
