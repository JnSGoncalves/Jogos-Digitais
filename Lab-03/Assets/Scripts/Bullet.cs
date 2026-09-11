using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.position.y > 6.0f || rb2d.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }
}
