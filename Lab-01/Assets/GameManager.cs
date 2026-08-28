using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ball ball;

    public AudioSource scoreSound;

    public TMP_Text winnerText;

    private TMP_Text scoreText;

    private int playerScore1 = 0;
    private int playerScore2 = 0;

    private bool gameOver = false;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();

        playerScore1 = 0;
        playerScore2 = 0;

        winnerText.gameObject.SetActive(false);

        UpdateScore();
    }

    public void Score(string wallID)
    {
        if (wallID.Equals("Top_Goal"))
        {
            playerScore1++;
        }
        else if (wallID.Equals("Bottom_Goal"))
        {
            playerScore2++;
        }

        scoreSound.Play();

        UpdateScore();

        if (playerScore1 >= 10)
        {
            EndGame("P1 VENCEU!");
        }
        else if (playerScore2 >= 10)
        {
            EndGame("P2 VENCEU!");
        }
    }

    void UpdateScore()
    {
        scoreText.text = $"{playerScore1} - {playerScore2}";
    }

    void EndGame(string message)
    {
        gameOver = true;

        winnerText.text = message;
        winnerText.gameObject.SetActive(true);

        ball.ResetBall();

        Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        playerScore1 = 0;
        playerScore2 = 0;

        winnerText.gameObject.SetActive(false);

        UpdateScore();

        gameOver = false;

        ball.ResetBall();
    }
}