using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rbplayer;
    [SerializeField] float speedPlayer;

    private void Awake()
    {
        rbplayer = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float eixoX = Input.GetAxis("Horizontal");
        rbplayer.linearVelocity = new Vector2(eixoX * speedPlayer, rbplayer.linearVelocity.y);
    }
}
