using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public Camera camera;
    public GameObject weapon;
    //private int lastDirection = 1;
    public bool attacking = false;
    //private bool hasAttacked = false;
    //private float timer;
    public float smooth = 1f;
    //private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        //timer = 0;
        //targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {


        //get the axis of the camera and make the player movements based on that
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        this.GetComponent<NavMeshAgent>().destination = transform.position + desiredMoveDirection;


        //rotate the player by moving the mouse
        //Get the Screen positions of the object
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Set the actual angle. Need to adjust it by 45 degrees because we're in isometric
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 45, 0f));


        if (Input.GetMouseButtonDown(0)) {
            weapon.GetComponent<Stick>().PerformAttack();
        }



        }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}