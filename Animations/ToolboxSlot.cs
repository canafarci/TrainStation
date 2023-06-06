using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxSlot : MonoBehaviour
{
    IStackTween _tweener;
    StackableItem _toolbox;
    private void Awake()
    {
        _tweener = GetComponent<IStackTween>();
        _toolbox = transform.GetChild(0).GetComponent<StackableItem>();
    }
    public IEnumerator PickUpItem(Transform target)
    {
        _toolbox.transform.parent = target;

        Vector3 pos = new Vector3(-0.170000002f, -0.289999992f, 1.32000005f);
        Vector3 rot = new Vector3(3.50843167f, 184.402863f, 1.86966121f);

        yield return StartCoroutine(_tweener.StackTween(_toolbox, pos, rot));
    }
    public IEnumerator DropItem(StackableItem toolbox)
    {
        _toolbox = toolbox;
        toolbox.transform.parent = transform;

        yield return StartCoroutine(_tweener.StackTween(_toolbox, Vector3.zero));
    }
}
