﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    bool wasPickedUp = false;
    int count = 0;
    bool once = true;
    Renderer myRenderer;
    // Start is called before the first frame update
    void Start() {
        myRenderer = this.GetComponent<Renderer>();
        Debug.Log("in enemy script");
    }

    // Update is called once per frame
    void Update() {
        var transform = this.GetComponent<Transform>();
        //transform.rotation *= Quaternion.Euler(100.0f * Time.deltaTime, 100.0f * Time.deltaTime, 0.0f);

        if (this.wasPickedUp) {
            transform.localScale *= 0.99f;
        }

    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Weapon")) {
            if (count == 0) {
                myRenderer.material.color = Color.yellow;
                count++;
                //Debug.Log("should change yellow");
            }
            else if (count == 1) {
                myRenderer.material.color = Color.red;
                count++;
                //Debug.Log("should change red");
            }
            else if (count == 2) {
                this.wasPickedUp = true;
                count++;
                //Debug.Log("should disappear");
            }
        }
    }

}

