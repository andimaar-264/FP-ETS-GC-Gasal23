using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Rigidbody cube_body;
    public float moveForce;
    public float jumpForce;
    private float x;
    public bool isOnPlatform = true;
    // public int jumpRemain = 2;

    void Start()
    {
        cube_body = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal") * moveForce * Time.deltaTime;

        cube_body.AddForce(this.transform.right * x);

        x = Input.GetAxis("Vertical") * moveForce * Time.deltaTime;

        cube_body.AddForce(this.transform.forward * x);
        
        if(Input.GetKey(KeyCode.Space) && isOnPlatform)
        {
            cube_body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isOnPlatform = false;
        }

        if(transform.position.y <= -10 || transform.position.x <= -20 || transform.position.x >= 20)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        // if(Input.GetKey(KeyCode.Space) && (jumpRemain >= 1))
        // {
        //     cube_body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //     jumpRemain -= 1;
        // }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
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