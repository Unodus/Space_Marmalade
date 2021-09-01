using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventDictionary
{
    public static Dictionary<string, UnityEvent> eventDictionary; // Dictionary of events that can be messaged
    public static Dictionary<string, UnityVector3Event> eventVec3Dictionary; // Dictionary of events that can be messaged
    public static void Init() // On creation, creates new events and sets name of colour palette
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            eventVec3Dictionary = new Dictionary<string, UnityVector3Event>();
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    } // Triggers an event in the dictionary with the given name

    // Triggers an event in the dictionary with the given name
    public static void StartListening(string eventName, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventName, thisEvent);
        }
    } // When called, allows the object to subscribe to when an event is triggered
    public static void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    } // When called, unsubscribes to an event




    //Vec3 Dictionary Controls:

    public class UnityVector3Event : UnityEvent<Vector3>
    {
    }

    public static void TriggerEvent(string eventName, Vector3 i)
    {
        UnityVector3Event thisEvent = null;
        if (eventVec3Dictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(i);
        }
    }
    public static void StartListening(string eventName, UnityAction<Vector3> listener)
    {
  
        if (eventVec3Dictionary.TryGetValue(eventName, out UnityVector3Event thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityVector3Event();
            thisEvent.AddListener(listener);
            eventVec3Dictionary.Add(eventName, thisEvent);
        }
    } // When called, allows the object to subscribe to when an event is triggered
    public static void StopListening(string eventName, UnityAction<Vector3> listener)
    {
        UnityVector3Event thisEvent = null;
        if (eventVec3Dictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    } // When called, unsubscribes to an event

}
