using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    public MyIntEvent Response2Int;
    public MyBoolEvent Response2Bool;
    public MyVector3Event Response2Vector3;
    public MyVector2IntEvent Response2Vector2Int;

    public MyStringEvent Respose2String;


    private void OnEnable()
    { 
        Event.RegisterListener(this); 
    }

    private void OnDisable()
    { 
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    { 
        Response.Invoke();
    }

    public void OnEventRaised(int fish)
    { 
        Response2Int.Invoke(fish);
    }

    public void OnEventRaised(bool fish)
    { 
        Response2Bool.Invoke(fish);
    }

    public void OnEventRaised(Vector3 fish)
    { 
        Response2Vector3.Invoke(fish);
    }

    public void OnEventRaised(Vector2Int fish)
    { 
        Response2Vector2Int.Invoke(fish);
    }

    public void OnEventRaised(string fish)
    { 
        Respose2String.Invoke(fish);
    }




}
