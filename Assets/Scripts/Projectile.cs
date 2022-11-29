using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public int damage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.paused)
        {
            rb.velocity = direction;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit;
        collision.gameObject.TryGetComponent<Unit>(out unit);
        if(unit == null)
        {
            return;
        }
        unit.TakeDamage(damage);
        Destroy(gameObject);
    }
}
