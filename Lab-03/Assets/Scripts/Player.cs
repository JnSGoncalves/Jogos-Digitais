using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float maxLimitX = 7.2f;
    private float minLimitX = -8.2f;
    [SerializeField] float speedPlayer;

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
    }
}
