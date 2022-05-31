using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlatforms : MonoBehaviour
{
    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
    foreach (Transform child in allChildren){
        GameObject cgo = child.gameObject;
        if(cgo.name.Contains("Level")){
            cgo.tag = "Level";
            Transform[] allChildren2 = GetComponentsInChildren<Transform>();
    foreach (Transform child2 in allChildren2){
        GameObject cgo2 = child2.gameObject;
            cgo2.tag = "Ground";
        }
            }
        }
    }
}
