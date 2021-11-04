using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SwapVisualizer : MonoBehaviour
{
    [SerializeField] private float _swapTime;

    public event UnityAction EmptyBodiesSwapped;

    public void SwapEmptyBodies(Vector3 firstStartPosition, Vector3 secondStartPosition, Body firstEmptyBody, Body secondEmptyBody)
    {
        firstEmptyBody.transform.position = firstStartPosition;
        secondEmptyBody.transform.position = secondStartPosition;
        StartCoroutine(VizualizeSwapping(firstStartPosition, secondStartPosition, firstEmptyBody, secondEmptyBody));
    }

    private IEnumerator VizualizeSwapping(Vector3 firstStartPosition, Vector3 secondStartPosition, Body firstEmptyBody, Body secondEmptyBody)
    {
        bool isSwapped = false;
        float speed = _swapTime != 0 ? (firstStartPosition - secondStartPosition).magnitude / _swapTime : float.MaxValue;
        while (isSwapped == false)
        {
            
            firstEmptyBody.transform.position = Vector3.MoveTowards(firstEmptyBody.transform.position, secondStartPosition, speed * Time.deltaTime);
            secondEmptyBody.transform.position = Vector3.MoveTowards(secondEmptyBody.transform.position, firstStartPosition, speed * Time.deltaTime);
            isSwapped = secondEmptyBody.transform.position == firstStartPosition && firstEmptyBody.transform.position == secondStartPosition;
            yield return null;
        }

        EmptyBodiesSwapped?.Invoke();
    }
}
