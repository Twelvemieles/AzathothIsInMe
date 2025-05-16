using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatchDMTools
{
    public static List<T> MixList<T>(List<T> list)
    {
        List<T> value = list;
        for (int i = 0; i < value.Count; i++)
        {
            int rnd = Random.Range(i, value.Count);
            T temp = value[i];
            value[i] = value[rnd];
            value[rnd] = temp;
        }
        return value;
    }
}
