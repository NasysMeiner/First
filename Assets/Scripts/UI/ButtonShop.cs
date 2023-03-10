using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Button _button;

    private int _price;
    private int _productId;
    private float _upgrate;
    private int _value;
    private bool _isPressed;
    private ShopDistributor _shopDistributor;
    private Balance _balance;

    private void OnEnable()
    {
        _button.onClick?.AddListener(UpgrateInId);
        _balance.ChangeBalance += ChangeButtonState;
        ChangeButtonState();
    }

    private void OnDisable()
    {
        _button.onClick?.RemoveListener(UpgrateInId);
        _balance.ChangeBalance -= ChangeButtonState;
    }

    public void Inst(int price, int productId, float upgrate, ShopDistributor shopDistributor, Balance balance, int value)
    {
        _price = price;
        _productId = productId;
        _upgrate = upgrate;
        _balance = balance;
        _shopDistributor = shopDistributor;
        _value = value;
    }

    public void ChangeButtonState()
    {
        if (_price <= _balance.Money)
            _button.interactable = true;
        else
            _button.interactable = false;
    }

    private void UpgrateInId()
    {
        _balance.ChangeMoney(-_price);
        _shopDistributor.Upgrade(_productId, _upgrate, _price, this);

        if(_isPressed == true)
            _price = 0;
        else
            _price += _value + Random.Range(0, _value);                  

        RenderPrice(_price);
    }
    private void RenderPrice(int price)
    {
        _button.GetComponentInChildren<TMP_Text>().text = price.ToString();
    }
}
