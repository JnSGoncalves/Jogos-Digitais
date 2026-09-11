using UnityEngine;

public class Enemie : MonoBehaviour {
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 4.0f;
    private float bulletTimer = 0.0f;
    private float bulletWaitTime = 0.0f;
    private float speed = 0.5f;
    public float bulletSpeed = 2.0f;
    public int points = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  

        var vel = rb2d.linearVelocity;
        vel.x = speed;
        rb2d.linearVelocity = vel;
        bulletWaitTime = Random.Range(5.0f, 50.0f);
    }

    // Update is called once per frame
    void Update()
    {        
        timer += Time.deltaTime;
        if (timer >= waitTime){
            ChangeState();
            timer = 0.0f;
        }

        bulletTimer += Time.deltaTime;
        if (bulletTimer >= bulletWaitTime && Random.Range(0, 10000) < 5){
            CrateBullet();
            bulletTimer = 0.0f;
            bulletWaitTime = Random.Range(5.0f, 50.0f);
        }
    }

    void ChangeState(){
        var vel = rb2d.linearVelocity;
        vel.x *= -1;
        rb2d.linearVelocity = vel;

        var y = rb2d.position.y;
        y -= 0.05f;
        rb2d.position = new Vector2(rb2d.position.x, y);
    }

    void CrateBullet(){
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject))) as GameObject;
        bullet.transform.position = new Vector2(rb2d.position.x, rb2d.position.y - 0.5f);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -bulletSpeed);
    }
}
