using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class ProductView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private ButtonShop _price;

    public void Render(Sprite icon, string title, int prise, int _numberUpgrate, float upgrate, ShopDistributor shopDistributor, Balance balance, int value)
    {
        _icon.sprite = icon;
        _title.text = title;
        _price.GetComponentInChildren<TMP_Text>().text = prise.ToString();
        _price.Inst(prise, _numberUpgrate, upgrate, shopDistributor, balance, value);
    }
}
