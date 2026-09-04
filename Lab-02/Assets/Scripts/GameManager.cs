using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStates{ Stop, Play, Pause, GameOver}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int Score;
    [SerializeField] Text ScoreText;
    public GameStates gameStates;

    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject ball, player;

    private void Awake()
    {
        SetScore(FindFirstObjectByType<Bricks>().point);
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameStates = GameStates.Stop;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && gameStates == GameStates.Stop)
        {
            StartGame();
        }
    }
    void StartGame()
    {
        FindAnyObjectByType<Ball>().StartBall();
    }

    public void LoadGameOver()
    {
        panels[0].SetActive(true);
        ball.SetActive(false);
        player.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadWin()
    {
        panels[0].SetActive(true);
        ball.SetActive(false);
        player.SetActive(false);
    }
    public void SetScore(int point)
    {
        ScoreText.text = "Score: " + Score;
        Score += point;
    }
}
