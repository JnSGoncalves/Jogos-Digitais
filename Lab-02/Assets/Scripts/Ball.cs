using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbBall;
    [SerializeField] float speedBall;

    private void Awake()
    {
        rbBall = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    public void StartBall()
    {
        rbBall.linearVelocity = Vector2.up * speedBall;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Palyer"))
        {
            float eixoX = transform.position.x - collision.transform.position.x / collision.collider.bounds.size.x;
            Vector2 diretion =  new Vector2(eixoX, 1).normalized;

            rbBall.linearVelocity = diretion * speedBall;
        }
        if (collision.gameObject.CompareTag("GameOver")){
            GameManager.instance.LoadGameOver();
        }
    }
}
