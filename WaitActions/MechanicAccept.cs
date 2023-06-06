using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicAccept : MonoBehaviour, IAcceptAction
{
    [SerializeField] NPCWaitQueue _queue;
    public IEnumerator OnAccept()
    {
        while (_queue.Peek() != null)
        {
            Mechanic mechanic = _queue.Peek().GetComponent<Mechanic>();

            _queue.Dequeue(mechanic);
            mechanic.PickItemAndFollowPlayer();
        }

        yield return null;
    }
}
