using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
// Write down your variables here
    public float score = 0;
    public static bool doubleJump = false;
    public gameObject scoreText;
    public void Update()
    {
        if(doubleJump) {
        }
    }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public static void IncrementScore(float score)
    {
        // TODO Increment score logic and win condition 
        score += score;
    }
    public void IncrementStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
