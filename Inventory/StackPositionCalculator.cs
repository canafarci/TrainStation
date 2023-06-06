using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Calculate Vector3 positions for the StackableItems
public class StackPositionCalculator : MonoBehaviour
{
    //Calculate positions for Inventory and world space stack locations
    public Vector3 CalculatePosition(IEnumerable<StackableItem> collection, StackableItem item)
    {
        float totalHeight = 0f;

        foreach (StackableItem si in collection)
        {
            totalHeight += si.ItemHeight;
        }

        return new Vector3(0f, totalHeight + item.ItemHeight / 2f, 0f);
    }

    //Recalculate positions after item is removed from the player Inventory
    public void RecalculatePositions(IEnumerable<StackableItem> collection)
    {
        float totalHeight = 0f;

        foreach (StackableItem si in collection)
        {
            si.transform.localPosition = new Vector3(0f, totalHeight + si.ItemHeight / 2f, 0f);
            totalHeight += si.ItemHeight;
        }
    }
}
