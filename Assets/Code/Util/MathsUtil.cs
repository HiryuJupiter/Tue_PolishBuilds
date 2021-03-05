using System.Collections;
using UnityEngine;

public static class MathsUtil
{
    public static float LinearInterpolation (float a, float b, float delta)
    {
        if (a > b)
        {
            a -= delta;
            if (a < b)
                a = b;
        }
        else
        {
            a += delta;
            if (a > b)
                a = b;
        }
        return a;
    }
}