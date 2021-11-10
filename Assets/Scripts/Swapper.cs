using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swapper : MonoBehaviour
{
    [SerializeField] private Body _firstEmptyBody;
    [SerializeField] private Body _secondEmptyBody;
    [SerializeField] private SwapVisualizer _swapVisualizer;

    private List<Body> _bodies;
    private bool _isSwapping;
    private Body _firstBody;
    private Body _secondBody;
    private BodypartType _bodypartType;

    public bool IsSwapping => _isSwapping;

    public event UnityAction Swapped;
    public event UnityAction BodypartsShuffled;
    public event UnityAction SwappedWithSame;

    private void OnDisable()
    {
        _swapVisualizer.EmptyBodiesSwapped -= OnSwappingVizualized;
    }

    private void SwapWithEmptyBodies(Body firstBody, Body secondBody, BodypartType bodypartType)
    {
        firstBody.TryExchangeBodyparts(_firstEmptyBody, bodypartType);
        secondBody.TryExchangeBodyparts(_secondEmptyBody, bodypartType);
    }

    private void OnSwappingVizualized()
    {
        _swapVisualizer.EmptyBodiesSwapped -= OnSwappingVizualized;
        SwapWithEmptyBodies(_secondBody, _firstBody, _bodypartType);
        _isSwapping = false;
        Swapped?.Invoke();
    }

    private void ShuffleBodyparts()
    {
        foreach (Body body in _bodies)
        {
            foreach (BodypartType bodypartType in (BodypartType[])Enum.GetValues(typeof(BodypartType)))
            {
                Body randomBody = _bodies[UnityEngine.Random.Range(0, _bodies.Count)];
                body.TryExchangeBodyparts(randomBody, bodypartType);
            }
        }

        BodypartsShuffled?.Invoke();
    }

    public void Swap(Bodypart firstBodypart, Bodypart secondBodypart)
    {
        _firstBody = _bodies.FirstOrDefault(body => body.HasBodypart(firstBodypart));
        _secondBody = _bodies.FirstOrDefault(body => body.HasBodypart(secondBodypart));
        _bodypartType = firstBodypart.PartType;

        if (_firstBody != null && _secondBody != null)
        {
            if (_firstBody != _secondBody)
            {
                _isSwapping = true;
                SwapWithEmptyBodies(_firstBody, _secondBody, firstBodypart.PartType);
                _swapVisualizer.EmptyBodiesSwapped += OnSwappingVizualized;
                _swapVisualizer.SwapEmptyBodies(_firstBody.transform.position, _secondBody.transform.position, _firstEmptyBody, _secondEmptyBody);
            }
            else
            {
                SwappedWithSame?.Invoke();
            }
        }
    }

    public void InitBodies(List<Body> bodies, Level level)
    {
        _bodies = bodies;
        foreach (Body body in _bodies)
        {
            body.Init(level);
        }

        _firstEmptyBody.Init(level);
        _secondEmptyBody.Init(level);

        ShuffleBodyparts();
    }
}
