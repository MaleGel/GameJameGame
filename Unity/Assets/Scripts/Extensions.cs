using System;
using UnityEngine;

namespace Assets.Scripts
{
    public static class IntExtensions
    {
        public static int ToSeconds(this int _)
        {
            return _ * 60;
        }
    }
}
