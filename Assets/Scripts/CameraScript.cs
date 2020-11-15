using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update() {
        //rotate camera by holding q and e
        if (Input.GetKey(KeyCode.Q)) {
            targetRotation *= Quaternion.AngleAxis(1, Vector3.up);
        }
        else if (Input.GetKey(KeyCode.E)) {
            targetRotation *= Quaternion.AngleAxis(1, Vector3.down);
        }
        //camera.transform.rotation = Quaternion.Lerp(transform.rotation, 
        //targetRotation, 
        //10 * smooth * Time.deltaTime);
        int DistanceAway = 30;
        Vector3 PlayerPOS = GameObject.Find("PlayerCube").transform.transform.position;
        transform.position = new Vector3(PlayerPOS.x + 25, PlayerPOS.y+15, PlayerPOS.z - 25);


    }

}
