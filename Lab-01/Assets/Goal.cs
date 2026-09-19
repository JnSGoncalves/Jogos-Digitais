using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        // gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Ball"))
        {
            GameManager.Instance.Score(gameObject.name);
            
        }
    }
}