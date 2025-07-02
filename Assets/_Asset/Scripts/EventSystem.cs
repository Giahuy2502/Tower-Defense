using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using _Asset.Scripts.MyAsset;
public static class EventSystem
{
    private static readonly Dictionary<EventName, Delegate> eventTable = new Dictionary<EventName, Delegate>();
    public static void Subscribe<T>(EventName eventName, Action<T> listener)
    {
        if (eventTable.TryGetValue(eventName, out var existingDelegate))
        {
            eventTable[eventName] = Delegate.Combine(existingDelegate, listener);
        }
        else
        {
            eventTable[eventName] = listener;
        }
    }

    public static void Subscribe(EventName eventName, Action listener)
    {
        if (eventTable.TryGetValue(eventName, out var existingDelegate))
        {
            eventTable[eventName] = Delegate.Combine(existingDelegate, listener);
        }
        else
        {
            eventTable[eventName] = listener;
        }
    }

    public static void Unsubscribe<T>(EventName eventName, Action<T> listener)
    {
        if (eventTable.TryGetValue(eventName, out var existingDelegate))
        {
            var currentDelegate = Delegate.Remove(existingDelegate, listener);
            if (currentDelegate == null)
                eventTable.Remove(eventName);
            else
                eventTable[eventName] = currentDelegate;
        }
    }

    public static void Unsubscribe(EventName eventName, Action listener)
    {
        if (eventTable.TryGetValue(eventName, out var existingDelegate))
        {
            var currentDelegate = Delegate.Remove(existingDelegate, listener);
            if (currentDelegate == null)
                eventTable.Remove(eventName);
            else
                eventTable[eventName] = currentDelegate;
        }
    }

    public static void Invoke<T>(EventName eventName, T eventData)
    {
        if (eventTable.TryGetValue(eventName, out var existingDelegate))
        {
            if (existingDelegate is Action<T> callback)
                callback.Invoke(eventData);
        }
    }

    public static void Invoke(EventName eventName)
    {
        if (eventTable.TryGetValue(eventName, out var existingDelegate))
        {
            if (existingDelegate is Action callback)
                callback.Invoke();
        }
    }
}
