using System.Numerics;
using UnityEngine;

public class Player2 : MonoBehaviour {
    private Vector3 initPos;
    public KeyCode moveUp = KeyCode.W;      // Move a raquete para cima
    public KeyCode moveDown = KeyCode.S;    // Move a raquete para baixo
    public KeyCode moveLeft = KeyCode.A;      // Move a raquete para cima
    public KeyCode moveRight = KeyCode.D;    // Move a raquete para baixo
    public KeyCode multplayerStarter = KeyCode.Space;
    public KeyCode multplayerFinisher = KeyCode.Escape; 
    public bool isMultplayer = false;

    public float boundY = 1f;               // Define os limites em Y
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();

        initPos = transform.position;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKey(multplayerStarter)){
            if (!isMultplayer)
                isMultplayer = true;
        }
        if (Input.GetKey(multplayerFinisher)){
            if (isMultplayer)
                isMultplayer = false;
        }

        Vector3 dir = rb2d.linearVelocity;
        Vector3 speedVec;
        Vector3 playerPos = transform.position;

        if (isMultplayer){
            if (Input.GetKey(moveUp)) {
                dir.y = 1;
            }
            else if (Input.GetKey(moveDown)) {
                dir.y = -1;                    
            }
            else {
                dir.y = 0;
            }

            if (Input.GetKey(moveRight)) { 
                dir.x = 1;
            }
            else if (Input.GetKey(moveLeft)) {
                dir.x = -1;                    
            }
            else {
                dir.x = 0;
            }
            dir.Normalize();
            speedVec = dir * keyboardSpeed;

        }else {
            speedVec = dir;
        }

        var vel = rb2d.linearVelocity;
        vel.x = speedVec.x;

        if (playerPos.y <= boundY && dir.y < 0) {
            playerPos.y = boundY;
            transform.position = playerPos;
            vel.y = 0;
        }
        else {
            vel.y = speedVec.y;
        }

        rb2d.linearVelocity = vel;
    }

    void Reset()
    {
        transform.position = initPos;
    }
}
