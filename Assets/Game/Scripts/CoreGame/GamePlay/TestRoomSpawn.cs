using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomSpawn : MonoBehaviour
{
    public GameObject parent;
    public GameObject objRoom;
    public Transform spawnPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == PlayerController.instance.controller.gameObject)
        {
            objRoom.SetActive(true);
            parent.SetActive(false);
            
            PlayerController.instance.controller.transform.position = spawnPosition.position;
            PlayerController.instance.camera2D.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y,PlayerController.instance.camera2D.transform.position.z);
            Action tempAction = delegate
            {
                PlayerController.instance.controller.transform.position = spawnPosition.position;
                //PlayerController.instance.camera2D.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y,PlayerController.instance.camera2D.transform.position.z);
                PlayerController.instance.controller.componentManager.rgbody2D.velocity= Vector2.zero;
                PlayerController.instance.camera2D.LocalPosition = new Vector3(spawnPosition.position.x, spawnPosition.position.y,PlayerController.instance.camera2D.transform.position.z);
            };
        
            ActionBufferManager.Instance.ActionDelayFrame(tempAction,1);
            
            PlayerController.instance.controller.componentManager.rgbody2D.velocity= Vector2.zero;
            
        }
    }
}
