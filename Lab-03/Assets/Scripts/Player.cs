using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private KeyCode shootKey = KeyCode.Space;
    private float maxLimitX = 7.2f;
    private float minLimitX = -8.2f;
    private float shootTimer = 0.0f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float speedPlayer = 10f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        // if(GameManager.instance.gameStates == GameStates.Stop)
        // {
        //     return;
        // }
        float eixoX = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(eixoX * speedPlayer, rb2d.linearVelocity.y);

        Vector2 position = rb2d.position;
        position.x = Mathf.Clamp(position.x, minLimitX, maxLimitX);
        rb2d.position = position;

        if (shootTimer <= 0.0f) {
            if (Input.GetKey(shootKey)) {
                Shoot();
                shootTimer = 1.0f;
            }
        }else {
            shootTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Resources.Load("Prefabs/PlayerBullet", typeof(GameObject))) as GameObject;
        bullet.transform.position = new Vector2(rb2d.position.x, rb2d.position.y);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, bulletSpeed);        
    }

    void OnTriggerEnter2D(Collider2D  collision) {
        Debug.Log("COLISÃO DETECTADA");

        if (collision.gameObject.CompareTag("Bullet")) {
            Debug.Log("BULLET DETECTADA");

            GameManager.instance.LoseLife();
            Destroy(collision.gameObject);
        }
    }
}
