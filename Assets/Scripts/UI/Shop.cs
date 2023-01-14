using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Product> _products;
    [SerializeField] private GameObject _containers;
    [SerializeField] private ProductView _tamplate;
    [SerializeField] private Balance _balance;
    [SerializeField] private ShopDistributor _shopDistributor;

    private void Start()
    {
        foreach(Product product in _products)
        {
            var view = Instantiate(_tamplate, _containers.transform);
            product.AddProduct(view, _balance, _shopDistributor);
        }
    }
}

[System.Serializable]

public class Product
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _title;
    [SerializeField] private int _price;
    [SerializeField] private int _numberUpgrate;
    [SerializeField] private float _upgrate;
    [SerializeField] private int _value;

    public void AddProduct(ProductView view, Balance balance, ShopDistributor shopDistributor)
    {
        view.Render(_icon, _title, _price, _numberUpgrate, _upgrate, shopDistributor, balance, _value);
    }
}
