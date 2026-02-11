using UnityEngine;
using System;

public class TriggerActor : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    public Action<bool> OnTriggered;

    private void OnTriggerEnter(Collider collider)
    {
        if (( mask & (1 << collider.gameObject.layer)) != 0)
        {
            OnTriggered?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (( mask & (1 << collider.gameObject.layer)) != 0)
        {
            OnTriggered?.Invoke(false);
        }
    }
}
