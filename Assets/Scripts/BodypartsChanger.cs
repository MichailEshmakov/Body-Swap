using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodypartsChanger : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _firstBodypart;
    [SerializeField] private SkinnedMeshRenderer _secondBodypart;

    [ContextMenu("ChangeParts")]
    private void ChangeParts()
    {
        Transform firstRootBone = _firstBodypart.rootBone;
        Transform[] firstBones = _firstBodypart.bones;
        Bounds firstBounds = _firstBodypart.localBounds;
        _firstBodypart.rootBone = _secondBodypart.rootBone;
        _firstBodypart.bones = _secondBodypart.bones;
        _firstBodypart.localBounds = _secondBodypart.localBounds;
        _secondBodypart.rootBone = firstRootBone;
        _secondBodypart.bones = firstBones;
        _secondBodypart.localBounds = firstBounds;
    }
}
