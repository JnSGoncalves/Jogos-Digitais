using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Player1 player1;
    public Player2 player2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        ball.ResetBall();
        player1.Reset();
        player2.Reset();

    }

    public void Score(string wallName)
    {
        GameObject score = GameObject.Find("Score");
        string txt = score.text;
        

        if (wallName.Equals(""))
        {
            
        }else if (wallName.Equals(""))
        {
            
        }
    }
}
