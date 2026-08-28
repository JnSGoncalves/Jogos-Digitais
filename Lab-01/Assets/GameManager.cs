using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

    public GameObject theBall;

    public TMP_Text scorePlayer1;
    public TMP_Text scorePlayer2;
    public TMP_Text winnerText;

    private bool gameOver = false;

    void Start()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        winnerText.gameObject.SetActive(false);

        UpdateScore();
    }

    public static void Score(string wallID)
    {
        string[] scores = scoreText.text.Split(" - ");

        PlayerScore1 = int.Parse(scores[0]);
        PlayerScore2 = int.Parse(scores[1]);

        if (wallName.Equals("Top_Goal"))
        {
            PlayerScore1++;
        }
        else if (wallName.Equals("Bottom_Goal"))
        {
            PlayerScore2++;
        }

        scoreSound.Play();
    }

    void Update()
    {
        UpdateScore();

        if (!gameOver)
        {
            if (PlayerScore1 >= 10)
            {
                EndGame("P1 VENCEU!");
            }
            else if (PlayerScore2 >= 10)
            {
                EndGame("P2 VENCEU!");
            }
        }
    }

    void UpdateScore()
    {
        scorePlayer1.text = PlayerScore1.ToString();
        scorePlayer2.text = PlayerScore2.ToString();
    }

    void EndGame(string message)
    {
        gameOver = true;

        winnerText.text = message;
        winnerText.gameObject.SetActive(true);

        theBall.SendMessage(
            "ResetBall",
            null,
            SendMessageOptions.RequireReceiver
        );

        Invoke("RestartGame", 2f);
    }

    void RestartGame()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        winnerText.gameObject.SetActive(false);

        UpdateScore();

        gameOver = false;

        theBall.SendMessage(
            "RestartGame",
            null,
            SendMessageOptions.RequireReceiver
        );
    }
}