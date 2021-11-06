using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorCharacterController
{
    public static class Params
    {
        public const string Completed = nameof(Completed); 
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Dance = nameof(Dance);
    }
}
