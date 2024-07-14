using UnityEngine;

public enum ProductType { water, enegeryDrink, ball }
[CreateAssetMenu(fileName = "Product Data", menuName = "Scriptable Object/Product Data", order = 0)]
public class ProductData : ScriptableObject
{
    public GameObject productPrefab;
    public ProductType productType;
    public int productPrice;

}
