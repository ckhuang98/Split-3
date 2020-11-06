using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class FireScript : MonoBehaviour
{
    int count = 0;
    float timer = 0.0f;
    public GameObject fire;
    public Sprite[] fireSprites;
    Vector3 position;
    int randomX;
    int randomY;
    int fireLevel = 0;
    public GameObject ash;
    bool done = false;
    bool spawned = false;
 
   
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        this.GetComponent<SpriteRenderer>().sprite = fireSprites[0];
/*         Debug.Log(Screen.width);
        Debug.Log(Screen.height); */

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        randomX = UnityEngine.Random.Range(-1, 2);
        randomY = UnityEngine.Random.Range(-1, 2);

        if(timer >= 1 && fireLevel ==0) {
            this.GetComponent<SpriteRenderer>().sprite = fireSprites[1];
            fireLevel++;
        }

        if (timer >= 2 && spawned == false) {
            if(fireLevel == 1) {
                this.GetComponent<SpriteRenderer>().sprite = fireSprites[2];
                fireLevel++;
            }
            SpawnNewFire();
            
            spawned = true;
        }

        if (timer >= 3 && fireLevel == 2) {
            // Debug.Log("fire level 4");
            this.GetComponent<SpriteRenderer>().sprite = fireSprites[3];
            fireLevel++;
        }

        if(timer >= 4) {
            if(done == false) {
                SpawnAsh();
            }
            timer = 0;
            done = true;
            spawned = false;
        }

    }

    private void OnMouseDown() {
        SpriteRenderer Color = this.GetComponent<SpriteRenderer>();
        Color.enabled = false;
        // Debug.Log("destroy this fire");
        Destroy(gameObject);
        Destroy(this);
    }

    private void SpawnNewFire() {
        // Debug.Log("fire should spread");
        count++;
        position = this.transform.position;
        if (randomX == -1) {
            position.x -= 1f;
        }
        else if (randomX == 1) {
            position.x += 1f;
        }
        if (randomY == -1) {
            position.y -= 1f;
        }
        else if (randomY == 1) {
            position.y += 1f;
        }
        var ground = GameObject.FindGameObjectWithTag("Ground");
        var grid = ground.GetComponent<Grid>();
        var tilePosition = grid.WorldToCell(position);

        if (position.x >= -10 && position.x <= 10  && position.y >= -5.5  && position.y <= 5.5) {
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
        }
        
    }

    private void SpawnAsh() {
        // Debug.Log("In spawn ash");
        var order = this.GetComponent<SpriteRenderer>().sortingOrder - 1;
        GameObject AshSprite = Instantiate(ash, this.transform.position, this.transform.rotation);
        //AshSprite.GetComponent<SpriteRenderer>().sortingOrder = order;
    }

}
