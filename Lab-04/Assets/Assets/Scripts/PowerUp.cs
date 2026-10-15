using UnityEngine;

// Power-up de slow-motion: anda para a esquerda; se a nave encostar, ativa o efeito.
public class PowerUp : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        Camera cam = Camera.main;
        float bordaEsquerda = cam.transform.position.x - cam.orthographicSize * cam.aspect - 1f;
        if (transform.position.x < bordaEsquerda)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerShip>() != null)
        {
            GameManager.Instance.ActivateSlowMotion();
            Destroy(gameObject);
        }
    }
}
