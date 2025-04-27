using System.Collections.Generic;
using UnityEngine;


public abstract class GameEventSO<T> : ScriptableObject
{
    public T Data;
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    public void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--) 
        {
            listeners[i].OnEventRaised(data); 
        }
    }

    public void RegisterListener(GameEventListener<T> listener) => listeners.Add(listener);


    public void UnregisterListener(GameEventListener<T> listener) => listeners.Remove(listener);

}