using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int enemiesCount = 10;
    private int lifes = 3;
    private int score = 0;
    private string gameOverStatus = "perdeu";
    [SerializeField] public bool isRunning = true;
    [SerializeField] private TextMeshProUGUI scoreObj;
    [SerializeField] private GameObject endGameButton;
    [SerializeField] private GameObject endGameText;
    
    private void Awake()
    {
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
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            isRunning = false;
        } else {
            scoreObj.text = $"Score: {score}";
            isRunning = true;
            endGameButton.SetActive(false);
            endGameText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SumScore(int scoreValue){
        score += scoreValue;
        scoreObj.text = $"Score: {score}";

        enemiesCount -= 1;
        if(enemiesCount <= 0){
            gameOverStatus = "ganhou";

            GameOver();
        }
    }

    public void LoseLife(){
        switch (lifes) {
            case 1:
                Destroy(GameObject.FindWithTag("Life1"));
                break;
            case 2:
                Destroy(GameObject.FindWithTag("Life2"));
                break;
            case 3:
                Destroy(GameObject.FindWithTag("Life3"));
                break;
        }

        lifes -= 1;
        if (lifes <= 0) {
            GameOver();
        }
    }

    public void ResetScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ChangeScene(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReturnTitleScreen(){
        isRunning = false;
        ChangeScene(0);
    }

    public void ReturnGameScreen(){
        isRunning = true;
        ChangeScene(1);
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

    public void GameOver(){
        isRunning = false;
        endGameText.GetComponent<TextMeshProUGUI>().text = 
            $"Fim de Jogo.\nVocê {gameOverStatus}!\nScore: {score}";

        endGameButton.SetActive(true);
        endGameText.SetActive(true);
    }
}
