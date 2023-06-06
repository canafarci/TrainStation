using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillImage : MonoBehaviour, IFillable
{
    [SerializeField] Image _image;

    public void SetFill(float maxValue, float value)
    {
        _image.fillAmount = (value / maxValue);
    }
}
