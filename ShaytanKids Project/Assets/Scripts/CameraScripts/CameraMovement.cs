using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    public void FollowPlayer()
    {
        this.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, this.transform.position.z);
    }
}
