using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int point = 10;
    public int dureza = 1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            dureza -= 1;
            if (dureza <= 0) {
                Destroy(gameObject);
                GameManager.instance.removeBricks();
                GameManager.instance.SetScore(point);
            }
        }
    }
    
}
