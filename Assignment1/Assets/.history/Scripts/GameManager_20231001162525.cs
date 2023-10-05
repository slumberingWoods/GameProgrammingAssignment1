using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
// Write down your variables here
    public float score = 0;
    public static bool doubleJump = false;
    public GameObject scoreText;
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
        score += score;
    }
    public void IncrementStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
