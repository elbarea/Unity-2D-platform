using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGround : MonoBehaviour
{
    
    public PlayerController player;
    public Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start () {
        player = GetComponentInParent<PlayerController>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
         if(col.gameObject.CompareTag("Platform")){
            rb2d.velocity = new Vector3(0f,0f, 0f);
            player.transform.parent = col.transform;
            player.grounded = true;
         }
    }


       

    void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground")){
            player.grounded = true;
        }
        if (col.gameObject.CompareTag("Platform")){
            
            player.transform.parent = col.transform;
            player.grounded = true;
        }
        
    }

    void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground")){
            player.grounded = false;
        }
        if (col.gameObject.CompareTag("Platform")){
            player.transform.parent = null;
            player.grounded = false;
        }
    }
}
