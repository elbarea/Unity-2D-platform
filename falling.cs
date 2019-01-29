using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling : MonoBehaviour
{
    public float fallDelay = 1f;
    public float respawnDelay = 2f;

    private Rigidbody2D rb2d;
    private PolygonCollider2D pc2d;
    private Vector3 start;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        start = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //se llama a los metodos con un delay
            Invoke("Fall",fallDelay);
            Invoke("Respawn", fallDelay+respawnDelay);
        }
    }

    void Fall()
    {
        rb2d.isKinematic = false;
        pc2d.isTrigger = true;
    }

    void Respawn()
    {
        rb2d.isKinematic = true;
        pc2d.isTrigger = false;
        transform.position = start;
        rb2d.velocity = Vector3.zero;
    }
}
