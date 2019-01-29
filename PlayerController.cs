﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    
    
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Se relacionan las variables Speed y Grounded de animacion con el código
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        
        //salto en el aire si no has saltado antes
        if (grounded)
        {
            doubleJump = true;
        }
        //salto normal
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                jump = true;
                doubleJump = true; //bool para doble salto
            }else if (doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
            
        }
    }
    void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;

        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }
        
        float h = Input.GetAxis("Horizontal");
        
        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, max: maxSpeed);

        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f,1f, 1f);
        }

        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f,1f, 1f);
        }

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        
    }
    void OnBecameInvisible()
    {
        transform.position = new Vector3(0,0,0);
    }
}
