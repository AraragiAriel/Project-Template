using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RNG
{
    public static Vector2 RandDir(float from, float to)
        => Random.Range(from, to).AngleToVector();

    public static Vector2 randDir => RandDir(0f, 360f);

    public static Vector2 randPos => randDir*Random.Range(0f, 1f);

    public static int randSign => Random.Range(0, 2) == 0 ?  1 : -1;

    public static bool randBool => ChanceCheck(.5f);

    public static bool ChanceCheck(float chance) => Random.Range(0f, 1f) <= chance;

    public static float RandMult(float range) => Random.Range(1f - range, 1f + range);
    public static float randMult => RandMult(.15f);

    public static float RandSignedMult(float range) => randSign*RandMult(range);
    public static float randSignedMult => randSign*randMult;

    public static int DrawWeightedIndex(List<float> chances)
    {
        if(chances.Count == 0)
        {
            Debug.LogWarning("Empty weighted list");
            return 0;
        }

        if(chances.Count == 1)
            return 0;

        float sum = chances.Sum();
        if(sum <= 0f)
        {
            Debug.LogWarning("Total weighted chance not positive");
            return 0;
        }

        float partial = 0f;
        float rand = Random.Range(0f, sum);
        foreach(int i in 0.To(chances.Count - 1, true))
        {
            partial += chances[i];
            if(partial >= rand)
                return i;
        }
        Debug.LogWarning("Couldn't draw position");
        return 0;
    }
}
