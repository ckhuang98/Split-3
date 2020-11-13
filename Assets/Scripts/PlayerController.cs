using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public Camera camera;
    public GameObject weapon;
    private int lastDirection = 1;
    private bool attacking = false;
    private float timer;
    public float smooth = 1f;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        timer = 0;
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        
        //Debug.Log(camera);

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
        

        if (Input.GetMouseButtonDown(0) && timer >=.5) {
            attacking = true;

            lastDirection = -1 * lastDirection;
            timer = 0;
        }
        if (attacking && timer <= .5) {
            weapon.transform.RotateAround(gameObject.transform.position, Vector3.up,
                    180 * lastDirection * Time.deltaTime);
        }
        timer += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Q)) {
            targetRotation *= Quaternion.AngleAxis(60, Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            targetRotation *= Quaternion.AngleAxis(60, Vector3.down);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, 
                                            targetRotation, 
                                            10 * smooth * Time.deltaTime);
    }

    void OnClick(GameObject weapon, GameObject Player) {

        
    }

}