using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void EventDelegate<TData>
(Event<TData> data);
public struct Event<TData>
{public TData data;}

static public class EventDispatcher<EventType>
{
    static public event Action<Event<EventType>> OnEvent;
    static public void Broadcast(EventType data)
    {
        if(OnEvent != null)
        {
            OnEvent(new Event<EventType>{data = data});
        }
    }
}

static public class EventDispatcherExit
{
    static public void Broadcast<T>(this T data)
    {
        EventDispatcher<T>.Broadcast(data);
    }
}

public class ModelPlayer
{
    public struct OnPlayerDiedEvent
    {
        //public PlayerInfo player;
    }
    public ModelPlayer()
    {
        EventDispatcher<OnPlayerDiedEvent>.OnEvent+=OnSomePlayerDied;
    }
    public void OnSomePlayerDied(Event<OnPlayerDiedEvent> obj)
    {

    }
    public void OneDie()
    {
        //new OnPlayerDiedEvent(){ModelPlayer = this}.Broadcast();
    }
}

public class ViewEvent //: MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
