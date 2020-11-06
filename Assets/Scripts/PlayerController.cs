using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject player;

    public Rigidbody2D rb;
    public Animator animator;
    public GameObject FireStarter;
    public int Range;
    public Text captureText;

    public Tilemap ground;
    
    
    private Vector2 lastMoveDirection;

    private Vector3Int playerPosition;

    public GameObject dirt;
    

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        Color alpha = captureText.color;
        alpha.a = 0;
        captureText.color = alpha;
    }

    // Update is called once per frame

    void Update()
    {
        

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x != 0 || movement.y !=0){
            lastMoveDirection = movement;
        }
/*
        if(lastMoveDirection.x == 0 && lastMoveDirection.y > 0){
            animator.SetBool("Idle_Back", true);
            animator.SetBool("Idle_Front", false);
            animator.SetBool("Idle_Right", false);
            animator.SetBool("Idle_Left", false);
        } else if(lastMoveDirection.x == 0 && lastMoveDirection.y < 0){
            animator.SetBool("Idle_Back", false);
            animator.SetBool("Idle_Front", true);
            animator.SetBool("Idle_Right", false);
            animator.SetBool("Idle_Left", false);
        } 
*/

        if (inRangeOfFS(FireStarter) && Input.GetKeyDown("space")) {
            Debug.Log("capture");
            capture(FireStarter);

        } else if(Input.GetKeyDown("space")){
            //Debug.Log("player position: " + playerPosition);
            var grid = ground.GetComponent<Grid>();
            var tilePosition = grid.WorldToCell(this.transform.position);
            GameObject DirtSprite = Instantiate(dirt, tilePosition, this.transform.rotation);
        }

        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Attack!");
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


    bool inRangeOfFS(GameObject FS) {
        if (FS) {
            Color alpha = captureText.color;
            var myPosition = this.transform.position;
            var enemyPosition = FS.transform.position;
            if (Vector3.Distance(myPosition, enemyPosition) < Range) {
                alpha.a = 1;
                captureText.color = alpha;
                return true;
            }
            else {
                alpha.a = 0;
                captureText.color = alpha;
                return false;
            };
        }
        return false;
    }

    void capture(GameObject FS) {
        //Destroy(FS.GetComponent<ScriptableObject>());
        FS.GetComponent<Renderer>().enabled = false;
        Destroy(FS);
        Color alpha = captureText.color;
        alpha.a = 0;
        captureText.color = alpha;
    }

/*     public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(){
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera){
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera){
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    } */
}
