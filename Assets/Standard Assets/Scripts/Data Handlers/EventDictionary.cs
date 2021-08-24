using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventDictionary
{
    public static Dictionary<string, UnityEvent> eventDictionary; // Dictionary of events that can be messaged
    public static void Init() // On creation, creates new events and sets name of colour palette
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
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

}
