using UnityEngine;

public class Player1 : MonoBehaviour {
    // Player 1
    public KeyCode moveUp = KeyCode.W;      // Move a raquete para cima
    public KeyCode moveDown = KeyCode.S;    // Move a raquete para baixo
    public KeyCode moveLeft = KeyCode.A;      // Move a raquete para cima
    public KeyCode moveRight = KeyCode.D;    // Move a raquete para baixo

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
