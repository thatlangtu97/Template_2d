using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public EventCollection eventCollectionData;
    protected Dictionary<int, IComboEvent> idEventTrigged = new Dictionary<int, IComboEvent>();
    public AudioClip Ambient;
    public float timeTrigger;
    public GameObject Boss;
    public float timeBoss;
    public bool started;
    public bool Entry;
    private void Awake()
    {
        this.RegisterListener(EventID.ENTRY_ROOM, (sender, param) => OnEntryRoom());
        this.RegisterListener(EventID.OPEN_DOOR, (sender, param) => FinishRoom());
    }
    
    private void FinishRoom()
    { 
        Entry = false;
        foreach (IComboEvent temp in idEventTrigged.Values)
        {
            temp.Recycle();
        }
    }

    void OnEntryRoom()
    {
        Entry = true;
    }
    void Start()
    {
        SoundManager.PlaySound(Ambient, false, 1, true, 1, SoundGroup.Global);

    }

    // Update is called once per frame
    void Update()
    {
        if (!Entry) return;
        timeTrigger += Time.deltaTime;

        if (eventCollectionData.EventCombo != null)
        {
            foreach (IComboEvent tempComboEvent in eventCollectionData.EventCombo)
            {
                if (timeTrigger > tempComboEvent.timeTrigger )
                {
                    if (!idEventTrigged.ContainsKey(tempComboEvent.id))
                    {
                        tempComboEvent.OnEventTrigger(null);
                        idEventTrigged.Add(tempComboEvent.id, tempComboEvent);
                    }
                    else
                    {
                        tempComboEvent.OnUpdateTrigger();
                    }
                }
            }
        }

        if (timeTrigger > timeBoss && !started)
        {
            Boss.SetActive(true);
            started = true;
        }
        
        
    }
}