using UnityEngine;

// Movimento (setas ou WASD) e tiro (barra de espaço) da nave.
public class PlayerShip : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;       // unidades por segundo
    public float padding = 0.6f;   // margem para a nave não sair da tela

    [Header("Tiro")]
    public GameObject bulletPrefab;   // arraste o prefab Bullet aqui
    public Transform firePoint;       // objeto filho na ponta da nave
    public float fireRate = 0.25f;    // segundos entre um tiro e outro

    private Camera cam;
    private float nextFireTime;

    void Start()
    {
        cam = Camera.main;

        if (bulletPrefab == null)
        {
            Debug.LogWarning("PlayerShip: arraste o prefab Bullet para o campo Bullet Prefab.");
        }
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");   // setas / A e D
        float v = Input.GetAxisRaw("Vertical");     // setas / W e S

        // normalized: na diagonal a nave não anda mais rápido que na reta.
        Vector3 direcao = new Vector3(h, v, 0f).normalized;
        transform.position += direcao * speed * Time.deltaTime;

        // Limita a posição aos limites visíveis da câmera.
        float metadeAltura = cam.orthographicSize;
        float metadeLargura = metadeAltura * cam.aspect;
        Vector3 centro = cam.transform.position;

        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, centro.x - metadeLargura + padding, centro.x + metadeLargura - padding);
        p.y = Mathf.Clamp(p.y, centro.y - metadeAltura + padding, centro.y + metadeAltura - padding);
        transform.position = p;
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        // Segurar espaço = tiro contínuo, respeitando o intervalo fireRate.
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Vector3 pos = (firePoint != null) ? firePoint.position : transform.position + Vector3.right * 0.7f;
            Instantiate(bulletPrefab, pos, Quaternion.identity);
        }
    }
}
