using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

    public GameObject theBall;

    public TMP_Text score;

    private bool gameOver = false;

    void Start()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        UpdateScore();
    }

    public static void Score(string wallID)
    {
        if (wallID == "TopGoal")
        {
            PlayerScore1++;
        }
        else
        {
            PlayerScore2++;
        }
    }

    void Update()
    {
        UpdateScore();

        if (!gameOver)
        {
            if (PlayerScore1 >= 10)
            {
                EndGame("VOCÊ VENCEU!");
            }
            else if (PlayerScore2 >= 10)
            {
                EndGame("VOCÊ PERDEU... :(");
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