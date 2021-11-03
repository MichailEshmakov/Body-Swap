using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BodypartCollider : MonoBehaviour
{
    [SerializeField] private Transform _bodypartsParent;
    [SerializeField] private BodypartType _bodypartType;
    [SerializeField] private Bodypart _bodypart;

    public Bodypart Bodypart => _bodypart;

    private void OnValidate()
    {
        if (_bodypartsParent != null)
        {
            Transform bodypartTransform = _bodypartsParent.Find(_bodypartType.ToString());
            if (bodypartTransform != null)
            {
                if (bodypartTransform.TryGetComponent(out Bodypart bodypart))
                {
                    _bodypart = bodypart;
                }
            }
        }

        if (_bodypart != null)
        {
            _bodypartType = _bodypart.PartType;
            _bodypartsParent = _bodypart.transform.parent;
        }
    }

    private void OnEnable()
    {
        _bodypart.Exchanging += OnBodypartExchanging;
    }

    private void OnDisable()
    {
        _bodypart.Exchanged -= OnBodypartExchanged;
        _bodypart.Exchanging -= OnBodypartExchanging;
    }

    private void OnBodypartExchanging(Bodypart newBodypart)
    {
        _bodypart.Exchanging -= OnBodypartExchanging;
        _bodypart = newBodypart;
        _bodypart.Exchanged += OnBodypartExchanged;
    }

    private void OnBodypartExchanged(Bodypart newBodypart)
    {
        _bodypart.Exchanged -= OnBodypartExchanged;
        _bodypart.Exchanging += OnBodypartExchanging;
    }
}
