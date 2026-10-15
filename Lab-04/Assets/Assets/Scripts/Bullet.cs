using UnityEngine;

// Tiro: anda para a direita e some ao sair da tela.
// Quem detecta o acerto é o Enemy (OnTriggerEnter2D).
public class Bullet : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        // Propositalmente NÃO multiplica por WorldSpeed: no slow-motion o tiro
        // continua na velocidade normal (vantagem do jogador).
        transform.position += Vector3.right * speed * Time.deltaTime;

        Camera cam = Camera.main;
        float bordaDireita = cam.transform.position.x + cam.orthographicSize * cam.aspect + 1f;
        if (transform.position.x > bordaDireita)
        {
            Destroy(gameObject);
        }
    }
}
