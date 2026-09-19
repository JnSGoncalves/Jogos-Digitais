using UnityEngine;

// Move o sprite para a esquerda e, quando ele sai da tela, "teletransporta" para
// o outro lado. Com dois sprites lado a lado, o fundo parece infinito.
// Coloque este script em TODOS os sprites de fundo (Farback01, Farback02, Stars...).
public class Parallax : MonoBehaviour
{
    // Velocidade da camada em unidades por segundo.
    // Camadas mais distantes = valores menores (ex.: Farback 0.2, Stars 0.6).
    public float parallaxEffect = 0.2f;

    private float length;   // largura do sprite em unidades do mundo (10.24)

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // GameManager.WorldSpeed vale 1 normalmente e cai durante o slow-motion.
        float passo = parallaxEffect * GameManager.WorldSpeed * Time.deltaTime;
        transform.position += Vector3.left * passo;

        // Saiu totalmente da tela? Pula 2 larguras para a direita, ficando
        // colado no sprite irmão. Somar (em vez de fixar x = length) preserva o
        // "resto" do movimento e evita micro-falhas entre os sprites.
        if (transform.position.x <= -length)
        {
            transform.position += Vector3.right * (2f * length);
        }
    }
}
