using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    [SerializeField] List<Body> bodies;
    [SerializeField] Pointer _pointer;

    private void OnEnable()
    {
        _pointer.Swapping += Swap;
    }

    private void OnDisable()
    {
        _pointer.Swapping -= Swap;
    }

    private void Swap(Bodypart firstBodypart, Bodypart secondBodypart)
    {
        Body body = bodies.FirstOrDefault(body => body.HasBodypart(firstBodypart));
        Body otherBody = bodies.FirstOrDefault(body => body.HasBodypart(secondBodypart));

        if (body != null && otherBody != null)
        {
            body.ExchangeBodyparts(otherBody, firstBodypart.PartType);
        }
        else
        {
            Debug.LogError("Not found body of bodypart");
        }
    }
}
