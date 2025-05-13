using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsRefresh: MonoBehaviour
{
    public static event System.Action<Attr> OnRefresh;

    public static void Refresh(Attr totalStats)
    {
        OnRefresh?.Invoke(totalStats);
    }

}