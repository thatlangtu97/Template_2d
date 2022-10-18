using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using Entitas;
using UnityEngine;

public class EventUpdate : MonoBehaviour
{
    private static EventUpdate _instance;
    public List<EventUpdateData> eventDatas = new List<EventUpdateData>();
    public static EventUpdate instance 
    {
        get
        {
            if (_instance == null)
            {
                GameObject soundManager = new GameObject("Event Update");
                _instance = soundManager.AddComponent<EventUpdate>();
            }
            return _instance;
        }
    }

    private void Update()
    {
        foreach (var temp in eventDatas)
        {
            if (temp.eventList == null) return;
            
            temp.timeTrigger += Time.deltaTime;
            foreach (IComboEvent tempComboEvent in temp.eventList)
            {
                if (temp.timeTrigger > tempComboEvent.timeTrigger )
                {
                    if (!temp.idEventTrigged.ContainsKey(tempComboEvent.id))
                    {
                        tempComboEvent.OnEventTrigger(temp.entity);
                        temp.idEventTrigged.Add(tempComboEvent.id, tempComboEvent);
                    }
                    else
                    {
                        tempComboEvent.OnUpdateTrigger();
                    }
                }
            }

            
            
        }

        for (int i = eventDatas.Count-1; i >=0; i--)
        {
            EventUpdateData temp = eventDatas[i];
            if (temp.idEventTrigged.Count == temp.eventList.Count)
            {
                eventDatas.RemoveAt(i);
            }
        }
    }

    public static void SetEvent(List<IComboEvent> eventList, GameEntity entity)
    {
        instance.eventDatas.Add(new EventUpdateData(eventList, entity));
    }

    public class EventUpdateData
    {
        public float timeTrigger;
        public List<IComboEvent> eventList;
        public GameEntity entity;
        public Dictionary<int, IComboEvent> idEventTrigged = new Dictionary<int, IComboEvent>();
        public EventUpdateData(){}

        public EventUpdateData(List<IComboEvent> eventList, GameEntity entity)
        {
            this.eventList = eventList;
            this.entity = entity;
        }
    }
}
