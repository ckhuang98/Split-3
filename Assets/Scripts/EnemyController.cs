using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    bool wasPickedUp = false;
    int count = 0;
    bool once = true;
    Renderer myRenderer;
    private NavMeshAgent navAgent;
    public LayerMask aggroLayerMask;
    private Collider[] withinAggroColliders;
    private float stealthRange = 0;
    public GameObject player;
    private float timer;
    
    public float agroRange = 0;
    
    // Start is called before the first frame update
    void Start() {
        myRenderer = this.GetComponent<Renderer>();
        //Debug.Log("in enemy script");
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        var transform = this.GetComponent<Transform>();
        //transform.rotation *= Quaternion.Euler(100.0f * Time.deltaTime, 100.0f * Time.deltaTime, 0.0f);

        if (this.wasPickedUp) {
            transform.localScale *= 0.99f;
            timer += Time.deltaTime;
        }

        if(timer >= 1) {
            Destroy(gameObject);
            Destroy(this);
        }
        var stealthRangeObject = GameObject.FindGameObjectWithTag("Stealth");
        if (Input.GetKey(KeyCode.Space)) {
           
            stealthRangeObject.transform.localScale = new Vector3(10, .5f, 10);
        }
        else {
            stealthRangeObject.transform.localScale = new Vector3(20, .5f, 20);
        }

        withinAggroColliders = Physics.OverlapSphere(transform.position, agroRange, aggroLayerMask);

        if(withinAggroColliders.Length > 0) {
            //Debug.Log("Found player");
            chasePlayer(player.transform.position);
        }
    }

    //this should no longer be used
    public void TakeDamage() {
        if (count == 0 ) {
            myRenderer.material.color = Color.yellow;
            count++;
            //Debug.Log("should change yellow");
        }
        else if (count == 1 ) {
            myRenderer.material.color = Color.red;
            count++;
            //Debug.Log("should change red");
        }
        else if (count == 2 ) {
            this.wasPickedUp = true;
            timer = 0;
            count++;
            //Debug.Log("should disappear");
        }
        
    }

    void chasePlayer(Vector3 destination) {
        //Debug.Log(player.transform.position);
        navAgent.SetDestination(destination);

    }

}

