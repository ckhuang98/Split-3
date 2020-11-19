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

    public int maxHealth = 200;
    public int currentHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update

    public int maxHunger = 100;
    public int currentHunger;

    public int maxThirst = 100;
    public int currentThirst = 100;

    public HungerBar hungerBar;
    public ThirstBar thirstBar;

    private const float TICK_TIMER_MAX = 1.0f;

    private float tickTimer;

    public Interactable focus;

    Camera cam;

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        //timer = 0;
        //targetRotation = transform.rotation;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHunger = maxHunger;
        hungerBar.SetMaxHunger(maxHunger);
        currentThirst = maxThirst;
        thirstBar.SetMaxThirst(maxThirst);

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        tick();

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
        Vector3 positionOnScreen = camera.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector3 mouseOnScreen = (Vector2)camera.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Set the actual angle. Need to adjust it by 45 degrees because we're in isometric* camera.transform.rotation.y
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle -45, 0f));


        if (Input.GetMouseButtonDown(0)) {
            weapon.GetComponent<Stick>().PerformAttack();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            // We create a ray
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
            Debug.Log("Attempting Pick Up");

			// If the ray hits
			if (Physics.Raycast(ray, out hit, 100))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
				{
					SetFocus(interactable);
				}
			}


        }



        }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    // void OnTriggerEnter(Collider collider) {
    //     if(collider.gameObject.CompareTag("Enemy")){
    //         TakeDamage(20);
    //     }

    //     if(collider.gameObject.CompareTag("Food")){
    //         currentHunger += 10;
    //         Destroy(collider.gameObject);
    //     }

    //     if(collider.gameObject.CompareTag("Water")){
    //         currentThirst += 10;
    //         Destroy(collider.gameObject);
    //     }

    // }

    void tick(){
        tickTimer += Time.deltaTime;
        if(tickTimer >= TICK_TIMER_MAX){
            tickTimer -= TICK_TIMER_MAX;
            currentHunger -= 1;
            currentThirst -= 3;
            if(currentHunger < 0 || currentThirst < 0){
                TakeDamage(10);
                currentHunger = 0;
                currentThirst = 0;
            }

            hungerBar.SetHunger(currentHunger);
            thirstBar.SetThirst(currentThirst);
            
        }
    }
    
    // Set our focus to a new focus
	void SetFocus (Interactable newFocus)
	{
		// If our focus has changed
		if (newFocus != focus)
		{
			// Defocus the old one
			if (focus != null)
				focus.OnDefocused();

			focus = newFocus;	// Set our new focus
		}
		
		newFocus.OnFocused(transform);
	}

	// Remove our current focus
	void RemoveFocus ()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;
	}
}