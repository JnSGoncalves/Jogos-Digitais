using UnityEngine;

public class Mothership : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minWaitTime = 5f;
    [SerializeField] private float maxWaitTime = 10f;
    [SerializeField] private float minX = -11f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private int points = 100;

    private Rigidbody2D rb2d;
    private float timer = 0f;
    private float waitTime = 0f;
    private int moveDirection = 1;
    private bool isMoving = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  

        var vel = rb2d.linearVelocity;
        vel.x = speed;
        rb2d.linearVelocity = vel;

        moveDirection = 1;
        StartWaiting();
    }

    private void Update()
    {
        if(GameManager.instance.isRunning == false) {
            rb2d.linearVelocity = Vector2.zero;
            return;
        }
        timer += Time.deltaTime;

        if (!isMoving)
        {
            if (timer >= waitTime)
            {
                StartMoving();
            }

            return;
        }

        if (timer >= waitTime){
            Vector2 position = rb2d.position;

            if (position.x <= minX)
            {
                rb2d.position = new Vector2(minX, position.y);
                rb2d.linearVelocity = Vector2.zero;
                moveDirection = 1;
                StartWaiting();
                return;
            }

            if (position.x >= maxX)
            {
                rb2d.position = new Vector2(maxX, position.y);
                rb2d.linearVelocity = Vector2.zero;
                moveDirection = -1;
                StartWaiting();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerBullet")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.instance.SumScore(points);
        }
    }

    private void StartWaiting()
    {
        isMoving = false;
        timer = 0f;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        rb2d.linearVelocity = Vector2.zero;
    }

    private void StartMoving()
    {
        isMoving = true;
        timer = 0f;
        waitTime = 5f; // Tempo de espera para verificar a posição novamente
        rb2d.linearVelocity = new Vector2(speed * moveDirection, 0f);
    }
}
