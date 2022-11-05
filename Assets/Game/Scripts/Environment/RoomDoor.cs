using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Animator anim;
    public string nameAnimOpen,nameAnimClose;
    public bool isOpen;
    public AudioClip openDoorClip, closeDoorClip;
    public float volume=0.5f;
    public float timeTriggerOpen;
    public float timeTriggerClose;
    private float timetrigger;
    public bool played;
    bool opened;
    private void Update()
    {
        timetrigger += Time.deltaTime;
        if (!played)
        {
            if (isOpen)
            {
                if (timetrigger >= timeTriggerOpen)
                {
                    if (closeDoorClip)
                    {
                        SoundManager.PlaySound(openDoorClip, false, volume, false, 1f);
                        played = true;
                    }
                }
            }
            else
            {
                if (timetrigger >= timeTriggerClose)
                {
                    if (openDoorClip)
                    {
                        SoundManager.PlaySound(closeDoorClip, false, volume, false, 1f);
                        played = true;
                    }
                }
                
            }
        }
    }

    private void Awake()
    {
        this.RegisterListener(EventID.CLOSE_DOOR, (sender,param) => CloseDoor());
        this.RegisterListener(EventID.OPEN_DOOR, (sender,param) => OpenDoor());
    }

    public void CloseDoor()
    {
        if (!opened)
        if (isOpen)
        {
            anim.Play(nameAnimClose);
            isOpen = false;
            played = false;
            opened = true;
            this.PostEvent(EventID.ENTRY_ROOM);
        }
    }
    public void OpenDoor()
    {
        if (!isOpen)
        {
            anim.Play(nameAnimOpen);
            isOpen = true;
            played = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<StateMachineController>() == null) return;
        OpenDoor();

    }
}
