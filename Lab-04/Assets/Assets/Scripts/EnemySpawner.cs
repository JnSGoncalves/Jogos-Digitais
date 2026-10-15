using UnityEngine;

// Cria inimigos (e, às vezes, power-ups) na borda direita da tela, em alturas aleatórias.
// Crie um objeto vazio "EnemySpawner" e adicione este script nele.
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public float spawnInterval = 1.5f;                 // segundos entre criações
    [Range(0f, 1f)] public float powerUpChance = 0.1f; // 0.1 = 10% das vezes vem um power-up
    public float marginY = 0.6f;                       // margem para não nascer colado na borda

    private float timer;

    void Update()
    {
        // Contando com WorldSpeed, no slow-motion nascem menos inimigos por segundo
        // e a densidade na tela continua parecida.
        timer += Time.deltaTime * GameManager.WorldSpeed;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        if (enemyPrefab == null) return;

        Camera cam = Camera.main;
        float metadeAltura = cam.orthographicSize;
        float metadeLargura = metadeAltura * cam.aspect;

        float x = cam.transform.position.x + metadeLargura + 1f;   // logo depois da borda direita
        float y = cam.transform.position.y + Random.Range(-metadeAltura + marginY, metadeAltura - marginY);

        bool sortearPowerUp = powerUpPrefab != null && Random.value < powerUpChance;
        GameObject prefab = sortearPowerUp ? powerUpPrefab : enemyPrefab;

        Instantiate(prefab, new Vector3(x, y, 0f), Quaternion.identity);
    }
}
