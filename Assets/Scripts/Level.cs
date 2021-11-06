using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Swapper _swapper;
    [SerializeField] private List<Celebrator> _celebrators;

    private int _comletedBodiesAmount = 0;

    public enum State
    {
        Initializing,
        Playing,
        Finish
    }

    private State _currentState;

    public State CurrentState =>_currentState;

    private void Awake()
    {
        _currentState = State.Initializing;
    }

    private void OnEnable()
    {
        _swapper.BodypartsShuffled += OnBodypartsShuffled;
        foreach (Celebrator celebrator in _celebrators)
        {
            celebrator.Celebrated += OnCelebrated;
        }    
    }

    private void OnCelebrated()
    {
        _comletedBodiesAmount++;
        if (_comletedBodiesAmount == _celebrators.Count)
        {
            Finish();
        }
    }

    private void Finish()
    {
        _currentState = State.Finish;
    }

    private void OnDisable()
    {
        _swapper.BodypartsShuffled -= OnBodypartsShuffled;
    }

    private void OnBodypartsShuffled()
    {
        _currentState = State.Playing;
    }
}
