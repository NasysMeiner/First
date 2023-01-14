using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] protected Image _image;

    public void OnChangeHealPoint(float value, float maxValue)
    {
        _image.fillAmount = value / maxValue;
    }
}
