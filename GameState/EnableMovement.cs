using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMovement : MonoBehaviour, ICustomUnlockBehaviour
{
    public void OnUnlock()
    {
        FindObjectOfType<Mover>().EnableMovement();
    }

    
}
