using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBody : Body
{
    protected override void RemovePart(Bodypart bodypart)
    {
        base.RemovePart(bodypart);
        bodypart.gameObject.layer = LayerMask.NameToLayer(Layers.Default);
    }

    protected override void AddPart(Bodypart bodypart)
    {
        base.AddPart(bodypart);
        bodypart.gameObject.layer = LayerMask.NameToLayer(Layers.Swapping);
    }
}
