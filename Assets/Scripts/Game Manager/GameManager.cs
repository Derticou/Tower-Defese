using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUI;
    public GameObject CompleteLevelUI;

    
    

    private void Start()
    {
        gameIsOver = false;
    }


    void Update()
    {

        if (gameIsOver)
            return;

        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }

    }

    void EndGame()
    {
        gameIsOver = true;
        Debug.Log("GameOver!");
        gameOverUI.SetActive(true);
    }

    public void winLevel()
    {
        

        gameIsOver = true;
        CompleteLevelUI.SetActive(true);
    }
}
