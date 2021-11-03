using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _raycastDistance;

    public event UnityAction<Bodypart, Bodypart> Swapping;

    private Bodypart _pointedBodypart;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _raycastDistance)
                && hit.collider.gameObject.TryGetComponent(out BodypartCollider bodypartCollider))
            {
                if (_pointedBodypart == null)
                {
                    Pick(bodypartCollider.Bodypart);
                }
                else 
                {
                    Swapping?.Invoke(_pointedBodypart, bodypartCollider.Bodypart);
                    Unpick();
                }
            }
        }
    }

    private void Pick(Bodypart bodypart)
    {
        _pointedBodypart = bodypart;
        bodypart.Select();
    }

    private void Unpick()
    {
        _pointedBodypart.Deselect();
        _pointedBodypart = null;
    }
}
