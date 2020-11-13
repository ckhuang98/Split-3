using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public Camera camera; 
    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        
        Debug.Log(camera);

        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();


        Vector3 desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;


        transform.position += (desiredMoveDirection * speed * Time.deltaTime);
        
        //var characterController = this.GetComponent<CharacterController>();
        //characterController.SimpleMove(desiredMoveDirection.normalized *speed );
       
    }

    

}