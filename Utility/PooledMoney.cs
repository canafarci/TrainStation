using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledMoney : MonoBehaviour
{
    public void ResetPosition()
    {
        transform.position = Vector3.one * 9999f;
    }
}
