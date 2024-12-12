using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController controller;
    public float speed = 12f;
    private float gravity = 9.8f;
    // Start is called before the first frame update

    Vector3 move;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(controller.isGrounded){

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        }
        move.y = move.y - gravity*Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime);
    }
    
}
