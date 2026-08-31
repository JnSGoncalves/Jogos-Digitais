using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;  
    //public AudioSource source;             // Define o corpo rigido 2D que representa a bola

    void GoBall()
{
    float direcaoY = Random.Range(0, 2) == 0 ? 1f : -1f;

    rb2d.linearVelocity = new Vector2(8f, 5f * direcaoY);
}
    void OnCollisionEnter2D(Collision2D coll)
{
    //source.Play(); // Toca o som de colisão
    if (coll.collider.CompareTag("Player"))
    {
        Rigidbody2D playerRb = coll.collider.attachedRigidbody;

        float velocidade = rb2d.linearVelocity.magnitude;
        Vector2 normal = coll.contacts[0].normal;
        Vector2 novaDirecao = Vector2.Reflect(
            rb2d.linearVelocity.normalized,
            normal
        );
        novaDirecao += playerRb.linearVelocity * 0.1f;
        novaDirecao.Normalize();
        rb2d.linearVelocity = novaDirecao * velocidade;
    }
}

    // Reinicializa a posição e velocidade da bola
    void ResetBall(){
        rb2d.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        //source = GetComponent<AudioSource>(); // Inicializa o objeto de áudio
        Invoke("GoBall", 2);    // Chama a função GoBall após 2 segundos
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}