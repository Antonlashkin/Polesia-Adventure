using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Player;
    public Transform Cam;
    void Update()
    {
        if(Player.transform.position.x > 7)
        {
            Cam.transform.position = new Vector3(7, Cam.transform.position.y, Cam.transform.position.z);
        }
         else if (Player.transform.position.x < -7)
        {
            Cam.transform.position = new Vector3(-7, Cam.transform.position.y, Cam.transform.position.z);
        }
        if (Player.transform.position.y > 7)
        {
            Cam.transform.position = new Vector3(Cam.transform.position.x, 7, Cam.transform.position.z);
        }
        if (Player.transform.position.y < -7)
        {
            Cam.transform.position = new Vector3(Cam.transform.position.x, -7, Cam.transform.position.z);
        }
    }
}
