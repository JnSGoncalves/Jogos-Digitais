using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int lifes = 3;
    private int score = 0;
    [SerializeField] public TextMeshProUGUI scoreObj;
    [SerializeField] private GameObject endGameButton;
    [SerializeField] private GameObject endGameText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumScore(int scoreValue){
        score += scoreValue;
        scoreObj.text = $"Score: {score}";
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

    public void ChangeScene(string sceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ReturnTitleScreen(){
        ChangeScene("0_Title");
    }

    public void ReturnGameScreen(){
        ChangeScene("1_Scene");
    }

    void GameOver(){
        endGameButton.SetActive(true);
        endGameText.SetActive(true);
    }
}
