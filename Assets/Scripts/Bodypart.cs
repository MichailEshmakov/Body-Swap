using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cakeslice;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class Bodypart : MonoBehaviour
{
    [SerializeField] private BodypartType _partType;
    [SerializeField] private Model _modelType;

    private Outline _outline;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    public BodypartType PartType => _partType;
    public Model ModelType => _modelType;

    public event UnityAction<Bodypart> Exchanging;
    public event UnityAction<Bodypart> Exchanged;

    private void OnValidate()
    {
        if (Enum.TryParse(gameObject.name, out BodypartType partType))
        {
            _partType = partType;
        }
        
        Model modelType = gameObject.GetComponentInParent<Model>();
        if (modelType != null)
        {
            _modelType = modelType;
        }
    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        _outline.enabled = true;
        _outline.enabled = false;
    }

    private void ExchangeBones(Bodypart other)
    {
        Transform firstRootBone = _skinnedMeshRenderer.rootBone;
        Transform[] firstBones = _skinnedMeshRenderer.bones;
        _skinnedMeshRenderer.rootBone = other._skinnedMeshRenderer.rootBone;
        _skinnedMeshRenderer.bones = other._skinnedMeshRenderer.bones;
        other._skinnedMeshRenderer.rootBone = firstRootBone;
        other._skinnedMeshRenderer.bones = firstBones;
    }

    public void Select()
    {
        _outline.enabled = true;
    }

    public void Deselect()
    {
        _outline.enabled = false;
    }

    public bool TryExchange(Bodypart other)
    {
        if (_partType == other.PartType)
        {
            ExchangeBones(other);
            Exchanging?.Invoke(other);
            other.Exchanging?.Invoke(this);
            Exchanged?.Invoke(other);
            other.Exchanged?.Invoke(this);
            return true;
        }

        Debug.LogError($"{gameObject.name} failed to change bodypart with {other.gameObject.name}. Parts must be of the same type");
        return false;
    }
}
