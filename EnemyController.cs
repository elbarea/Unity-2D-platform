using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 1f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, max: maxSpeed);

        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (rb2d.velocity.x < 0.01 && rb2d.velocity.x > -0.01)
        {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }

        if (speed < 0)
        {
            transform.localScale = new Vector3(5f, 5f, 1f);
        }
        
        if (speed > 0)
        {
            transform.localScale = new Vector3(-5f, 5f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transform.position.y < other.transform.position.y)
            {
                other.SendMessage("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                other.SendMessage("EnemyKnockBack", transform.position.x);
            }
        }
    }
}
