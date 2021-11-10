using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private Swapper _swapper;
    private List<Celebrator> _celebrators;
    private State _currentState = State.Initializing;
    private int _comletedBodiesAmount = 0;

    public State CurrentState => _currentState;

    public event UnityAction Inited;
    public event UnityAction Finishing;

    public enum State
    {
        Initializing,
        Playing,
        Finish
    }

    private void OnDisable()
    {
        _swapper.BodypartsShuffled -= OnBodypartsShuffled;
        foreach (Celebrator celebrator in _celebrators)
        {
            celebrator.Celebrated -= OnCelebrated;
        }
    }

    private void OnCelebrated()
    {
        _comletedBodiesAmount++;
        if (_comletedBodiesAmount >= _celebrators.Count)
        {
            Finish();
        }
    }

    private void Finish()
    {
        _currentState = State.Finish;
        _comletedBodiesAmount = 0;
        Finishing?.Invoke();
    }

    private void OnBodypartsShuffled()
    {
        _currentState = State.Playing;
    }

    private void SetCelebrators(List<Celebrator> celebrators)
    {
        if (_celebrators != null)
        {
            foreach (Celebrator celebrator in _celebrators)
            {
                celebrator.Celebrated -= OnCelebrated;
            }
        }

        _celebrators = celebrators;

        foreach (Celebrator celebrator in _celebrators)
        {
            celebrator.Celebrated += OnCelebrated;
        }
    }

    private void SetSwapper(Swapper swapper)
    {
        if (_swapper != null)
        {
            _swapper.BodypartsShuffled -= OnBodypartsShuffled;
        }
        _swapper = swapper;
        _swapper.BodypartsShuffled += OnBodypartsShuffled;
    }

    public void Init(List<Celebrator> celebrators, Swapper swapper)
    {
        _currentState = State.Initializing;
        SetCelebrators(celebrators);
        SetSwapper(swapper);
        Inited?.Invoke();
    }
}
