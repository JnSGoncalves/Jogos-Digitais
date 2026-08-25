using UnityEngine;

public class Player : MonoBehaviour {
    // Player 2
    public KeyCode moveUp = KeyCode.moveUp;      // Move a raquete para cima
    public KeyCode moveDown = KeyCode.moveDown;    // Move a raquete para baixo
    public KeyCode moveLeft = KeyCode.moveLeft;      // Move a raquete para cima
    public KeyCode moveRight = KeyCode.moveRight;    // Move a raquete para baixo

    public float speed = 100.0f;             // Define a velocidade da raquete
    public float boundY = 0f;               // Define os limites em Y
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        Vector3 playerPos = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = mousePos - playerPos;
        dir.Normalize();

        Vector3 speedVec = dir * speed;

        var vel = rb2d.velocity;
        vel.x = speedVec.x;
        vel.y = speedVec.y;
        rb2d.velocity = vel;
    }
}
