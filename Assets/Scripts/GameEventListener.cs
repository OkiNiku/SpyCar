using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener<T> : MonoBehaviour
{
    [SerializeField]
    private GameEventSO<T> gameEvent;
    [SerializeField]
    private UnityEvent<T> onEvent;

    private void OnEnable() => gameEvent.RegisterListener(this);
    private void OnDisable() => gameEvent.UnregisterListener(this);


    public void OnEventRaised(T data) => onEvent?.Invoke(data);

}