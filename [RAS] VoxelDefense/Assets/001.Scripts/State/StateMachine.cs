using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateMachin<T> where T : class
{
    private T entity;
    private State<T> currentState;
    private bool isChanging;

    public StateMachin(T _entity,State<T> _currentState)
    {
        entity = _entity;
        currentState = _currentState;
        currentState.StartState(entity);
        isChanging = false;
    }

    public void ChangeState(State<T> _state)
    {
        isChanging = true;
        currentState.EndState();
        currentState = _state;
        currentState.StartState(entity);
        isChanging = false;
    }

    public void UpdateState()
    {
        if (isChanging) return;
        currentState?.UpdateState();
    }

    public void FixedUpdateState()
    {
        if (isChanging) return;
        currentState?.FixedUpdateState();
    }
}

public abstract class State<T> where T : class
{
    protected T entity;
    public virtual void StartState(T _entity)
    {
        entity = _entity;
    }

    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void EndState();
}
