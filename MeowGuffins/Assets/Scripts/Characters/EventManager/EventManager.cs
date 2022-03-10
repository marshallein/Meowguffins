using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction CoinCollected;
    public static void OnCoinCollected() => CoinCollected?.Invoke();
}
