using UnityEngine;

// Inimigo: anda para a esquerda.
// - Encostou em um tiro: soma pontos e ambos somem.
// - Encostou na nave: o jogador perde uma vida e o inimigo some.
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int points = 10;

    private bool morto;   // evita contar duas vezes se duas colisões ocorrerem no mesmo frame

    void Update()
    {
        // WorldSpeed faz o inimigo desacelerar durante o slow-motion.
        transform.position += Vector3.left * speed * GameManager.WorldSpeed * Time.deltaTime;

        Camera cam = Camera.main;
        float bordaEsquerda = cam.transform.position.x - cam.orthographicSize * cam.aspect - 1f;
        if (transform.position.x < bordaEsquerda)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (morto) return;

        if (other.GetComponent<Bullet>() != null)
        {
            morto = true;
            GameManager.Instance.AddScore(points);
            Destroy(other.gameObject);   // destrói o tiro
            Destroy(gameObject);         // destrói o inimigo
        }
        else if (other.GetComponent<PlayerShip>() != null)
        {
            morto = true;
            GameManager.Instance.LoseLife();
            Destroy(gameObject);
        }
    }
}
