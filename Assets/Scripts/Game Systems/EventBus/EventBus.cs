using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventBus 
{
    public Dictionary<Type, List<EventHandler>> subscriberByType;

    public EventBus()
    {
        subscriberByType = new Dictionary<Type, List<EventHandler>>(); //initialize the dictionary so we dont get an error
    }

    public void Subscribe<T>(EventHandler eventHandler) where T : EventArgs //put constraints on the subscribe methods so it can only get event types
    {
        var type = typeof(T);
        if (!subscriberByType.TryGetValue(type,out var subscribers)) //if the passed in eventhandler is not in the dictonary add it
        {
            subscriberByType.Add(type, new List<EventHandler>());
        }

        subscriberByType[type].Add(eventHandler);
    }
    public void UnSubscribe<T>(EventHandler eventHandler) where T : EventArgs
    {
        var type = typeof(T);
        if (!subscriberByType.TryGetValue(type, out var subscribers)) // if passed in eventhanler is in the dictionary remove it
        {
            subscriberByType[type].Remove(eventHandler);
        }

    }
    public void Publish(object sender, EventArgs eventArgs)
    {
        if (subscriberByType.TryGetValue(eventArgs.GetType(), out var subscribers))
        {
            foreach (var subscriber in subscribers) //send the eventargs data to each subscribed member in the dictionary
            {
                subscriber(sender, eventArgs);
            }
        }
    }

}
