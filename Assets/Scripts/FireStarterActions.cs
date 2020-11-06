using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireStarterActions : MonoBehaviour
{
    private Transform thisTransform;

    public Animator animator;

    public float moveSpeed = 5f;

    private Vector2 moveTime = new Vector2(1, 4);
    private float moveTimeCount = 0;

    public GameObject fire;
    public Tilemap ground;

    public Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left,
        Vector3.up, Vector3.down};
    private int currMoveDirection;

    
    float fireTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.transform;

        moveTimeCount = Random.Range(moveTime.x, moveTime.y);

        chooseMoveDirection();

        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        thisTransform.position += moveDirections[currMoveDirection] * Time.deltaTime * moveSpeed;

        if (moveTimeCount > 0)
        {
            moveTimeCount -= Time.deltaTime;
        } else
        {
            moveTimeCount = Random.Range(moveTime.x, moveTime.y);
            chooseMoveDirection();
        }

        if (thisTransform.position.x >= 11.5f)
        {
            moveTimeCount = Random.Range(moveTime.x, moveTime.y);
            currMoveDirection = 1;
        } else if (thisTransform.position.x <= -10.5)
        {
            moveTimeCount = Random.Range(moveTime.x, moveTime.y);
            currMoveDirection = 0;
        }
        else if (thisTransform.position.y <= -5.25)
        {
            moveTimeCount = Random.Range(moveTime.x, moveTime.y);
            currMoveDirection = 2;
        }
        else if (thisTransform.position.y >= 5)
        {
            moveTimeCount = Random.Range(moveTime.x, moveTime.y);
            currMoveDirection = 3;
        }

        animator.SetFloat("Horizontal", moveDirections[currMoveDirection].x);
        animator.SetFloat("Vertical", moveDirections[currMoveDirection].y);
        animator.SetFloat("Speed", moveSpeed);

        if (fireTimer >= .5)
        {
            var ground = GameObject.FindGameObjectWithTag("Ground");
            var grid = ground.GetComponent<Grid>();
            var tilePosition = grid.WorldToCell(thisTransform.position);
            bool exists = false;
            GameObject[] existingFires = GameObject.FindGameObjectsWithTag("FireSprite");
            GameObject[] existingDirt = GameObject.FindGameObjectsWithTag("Dirt");
            for (int i = 0; i < existingFires.Length; i++) {
                if (existingFires[i].transform.position == tilePosition) {
                    exists = true;
                    break;
                }
            }
            for (int i = 0; i < existingDirt.Length; i++) {
                
                if (grid.WorldToCell(existingDirt[i].transform.position) == tilePosition) {
                    
                    exists = true;
                    break;
                }
            }
            if (!exists) {
                Instantiate(fire, tilePosition, this.transform.rotation);
            }
            
            fireTimer = 0.0f;
        }
        
    }

    void chooseMoveDirection()
    {
        currMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
    }
}
