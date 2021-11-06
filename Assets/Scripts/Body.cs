using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Body : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private List<Bodypart> _parts;

    public event UnityAction Completed;

    private void OnValidate()
    {
        if (CheckPartsTypes() == false)
        {
            Debug.LogError($"{gameObject.name} hasn't correct bodyparts");
        }
    }

    [ContextMenu("Find bodyparts in children")]
    private void FindBodypartsInChildren()
    {
        _parts = transform.GetComponentsInChildren<Bodypart>().ToList();
    }

    private bool CheckCompleteness()
    {
        return _parts.All(part => part.ModelType == _parts.First().ModelType);
    }

    private bool TryChangeBodypart(Bodypart newPart)
    {
        Bodypart previousPart = _parts.FirstOrDefault(part => part.PartType == newPart.PartType);
        if (previousPart != null)
        {
            _parts.Remove(previousPart);
            _parts.Add(newPart);
            if (_level.CurrentState == Level.State.Playing && CheckCompleteness())
            {
                Completed?.Invoke();
            }

            return true;
        }

        return false;
    }

    private bool CheckPartsTypes()
    {
        BodypartType[] partTypes = (BodypartType[])Enum.GetValues(typeof(BodypartType));

        foreach (BodypartType partType in partTypes)
        {
            if (_parts.Any(part => part.PartType == partType) == false)
            {
                return false;
            }
        }

        return partTypes.Length == _parts.Count;
    }

    public bool TryExchangeBodyparts(Body other, BodypartType bodypartType)
    {
        Bodypart thisBodypart = _parts.FirstOrDefault(part => part.PartType == bodypartType);
        Bodypart otherBodypart = other._parts.FirstOrDefault(part => part.PartType == bodypartType);
        if (thisBodypart != null && otherBodypart != null)
        {
            if (thisBodypart.TryExchange(otherBodypart))
            {
                if (TryChangeBodypart(otherBodypart) && other.TryChangeBodypart(thisBodypart))
                {
                    return true;
                }
            }
        }

        Debug.LogError($"Bodyparts exchanging is failed. {gameObject.name} {other.gameObject.name} {bodypartType}");
        return false;
    }

    public bool HasBodypart(Bodypart bodypart)
    {
        return _parts.Contains(bodypart);
    }
}
