using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public int FPS;
    void FollowObject(GameObject obj){
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -10);
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    // Update is called once per frame
    void Update()
    {
        FollowObject(player);
    }
}
