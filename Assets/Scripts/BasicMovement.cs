using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveForce;
    public float jumpForce;
    private float x;
    public bool isOnGround = true;
    // public int jumpRemain = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal") * moveForce * Time.deltaTime;

        rb.AddForce(this.transform.right * x);

        x = Input.GetAxis("Vertical") * moveForce * Time.deltaTime;

        rb.AddForce(this.transform.forward * x);
        
        if(Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if(transform.position.y <= -10 || transform.position.x <= -20 || transform.position.x >= 20)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        // if(Input.GetKey(KeyCode.Space) && (jumpRemain >= 1))
        // {
        //     rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //     jumpRemain -= 1;
        // }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }
    }

    // private void OnCollisionEnter(Collision collision) 
    // {
    //     if (collision.gameObject.CompareTag("Platform"))
    //     {
    //         jumpRemain = 1;
    //     }
    // }
}