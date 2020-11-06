using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) { // if left button pressed...
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                // the object identified by hit.transform was clicked
                Debug.Log("raycast");
                var fire = hit.collider.transform.parent.gameObject;
                Debug.Log(fire);
                var rend = fire.GetComponent<SpriteRenderer>();
                if(rend != null) {
                    rend.enabled = false;
                    Destroy(fire);
                }
            }
        }
    }
    
}
