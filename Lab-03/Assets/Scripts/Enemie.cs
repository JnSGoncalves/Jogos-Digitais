using UnityEngine;

public class Enemie : MonoBehaviour {
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 4.0f;
    private float bulletTimer = 0.0f;
    private float bulletWaitTime = 0.0f;
    private float speed = 0.5f;
    public float bulletSpeed = 1.0f;
    public int points = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  

        var vel = rb2d.linearVelocity;
        vel.x = speed;
        rb2d.linearVelocity = vel;
        bulletWaitTime = Random.Range(5.0f, 30.0f);
    }

    // Update is called once per frame
    void Update()
    {        
        if(GameManager.instance.isRunning == false) {
            rb2d.linearVelocity = Vector2.zero;
            return;
        }
        timer += Time.deltaTime;
        if (timer >= waitTime){
            ChangeState();
            timer = 0.0f;
        }

        bulletTimer += Time.deltaTime;
        if (bulletTimer >= bulletWaitTime && Random.Range(0, 100) < 2){
            CrateBullet();
            bulletTimer = 0.0f;
            bulletWaitTime = Random.Range(5.0f, 30.0f);
        }
    }

    void ChangeState(){
        var vel = rb2d.linearVelocity;
        vel.x *= -1;
        rb2d.linearVelocity = vel;

        var y = rb2d.position.y;
        y -= 0.2f;
        rb2d.position = new Vector2(rb2d.position.x, y);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerBullet")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.instance.SumScore(points);

            return;
        }

        if (collision.gameObject.CompareTag("BottomLimit")) {
            GameManager.instance.GameOver();
        }
    }

    void CrateBullet(){
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject))) as GameObject;
        bullet.transform.position = new Vector2(rb2d.position.x, rb2d.position.y - 0.5f);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -bulletSpeed);
    }
}
