using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject stick;
    public float smooth = 1f;
    private Quaternion targetRotation;
    private Vector3 offset;
    public float x = 25;
    public float y = 40;
    public float z = -25;
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.eulerAngles = new Vector3(24, -45, 0);
        //targetRotation.eulerAngles = new Vector3(24, -45, 0);
        var PlayerPOS = player.transform.position;
        offset = new Vector3 (PlayerPOS.x + 15, PlayerPOS.y + 10, PlayerPOS.z - 15);
    }

    // Update is called once per frame
    
    void Update() {
        //rotate camera by holding q and e
        if (Input.GetKey(KeyCode.Q)) {
            //targetRotation *= Quaternion.AngleAxis(1, Vector3.up);
            //offset = Quaternion.AngleAxis(1, new Vector3 (0, 1, 0)) * offset;
            //transform.RotateAround(player.transform.position, new Vector3 (0, 1, 0), 90 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E)) {
            //targetRotation *= Quaternion.AngleAxis(1, Vector3.down);
            //offset = Quaternion.AngleAxis(1, new Vector3(0, -1, 0)) * offset;
            //transform.RotateAround(player.transform.position, new Vector3 (0, -1, 0), 90 * Time.deltaTime);
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, 
            //targetRotation, 
            //10 * smooth * Time.deltaTime);
        int DistanceAway = 30;
        transform.position = player.transform.position + offset + new Vector3(x, y, z);
        //transform.LookAt(player.transform.position);
        //Debug.Log(PlayerPOS);
        //transform.position = new Vector3(PlayerPOS.x + 25, PlayerPOS.y+15, PlayerPOS.z - 25);


    }

}
