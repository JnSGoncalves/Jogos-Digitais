using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStates{ Stop, Play, GameOver, Win}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int Score;

    [SerializeField] Text ScoreText;

    public GameStates gameStates;

    [SerializeField] GameObject[] panels;

    [SerializeField] GameObject ball, player;

    [SerializeField] int totalBricks = 45;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
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
        LoadWin();
        if (Input.GetButtonDown("Jump") && gameStates == GameStates.Stop)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStates = GameStates.Play;
        FindAnyObjectByType<Ball>().StartBall();
    }

    public void removeBricks()
    {
        totalBricks--;
    }

    public void LoadGameOver()
    {
        gameStates = GameStates.GameOver;
        panels[0].SetActive(true);
        ball.SetActive(false);
        player.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ProxNvButton()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadWin()
    {
        if(totalBricks <= 0)
        {
            gameStates = GameStates.Win;
            panels[1].SetActive(true);
            ball.SetActive(false);
            player.SetActive(false);
        }  
    }

    public void SetScore(int point)
    {
        ScoreText.text = "Score: " + Score;
        Score += point;
    }
}
