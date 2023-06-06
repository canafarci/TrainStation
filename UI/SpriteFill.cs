using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFill : MonoBehaviour, IFillable
{
    [SerializeField] Renderer _slider;
    Material _mat;
    public void SetFill(float maxValue, float value)
    {
        //0 max, 1 min;
        if (_mat == null)
            _mat = _slider.material;
        _mat.SetFloat("_ClipUvUp", (value / maxValue));
    }
}
