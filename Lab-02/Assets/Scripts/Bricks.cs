using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int point = 10;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
            GameManager.instance.SetScore(point);
        }
    }
    
}
