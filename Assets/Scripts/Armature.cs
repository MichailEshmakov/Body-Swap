using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Armature : MonoBehaviour
{
    [SerializeField] private Transform _rootBone;
    [SerializeField] private List<Transform> _bones;
    [SerializeField] private List<SkinnedMeshRenderer> renderers;

    [ContextMenu("Init")]
    private void Init()
    {
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            List<Transform> newBones = new List<Transform>();
            foreach (Transform bone in renderer.bones)
            {
                Transform newBone = _bones.FirstOrDefault(newBone => newBone.name == bone.name);
                if (newBone != null)
                {
                    newBones.Add(newBone);
                }
                else
                {
                    Debug.LogError($"Bone + {bone.name} is not found");
                }
            }

            renderer.bones = newBones.ToArray();
            renderer.rootBone = _rootBone;
        }
    }
}
