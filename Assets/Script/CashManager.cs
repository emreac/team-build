using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    private int coins;
    private string keyCoins = "keyCoins";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }
    public void AddCoin(int price)
    {
        coins += price;
        DisplayCash();
    }
    private void SpendCoint(int price)
    {
        coins -= price;
        DisplayCash();
    }
    public bool TryBuyUnit(int price)
    {
        if (GetCoins() >= price)
        {
            //Spend money
            SpendCoint(price);
            return true;
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadCash();
        DisplayCash();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetCoins()
    {
        return coins;
    }
    private void DisplayCash()
    {
        UIManager.Instance.ShowCoinCountOnScreen(coins);
        SaveCash();
    }
    private void LoadCash()
    {
        coins = PlayerPrefs.GetInt(keyCoins, 0);
    }
    private void SaveCash()
    {
        PlayerPrefs.SetInt(keyCoins, coins);

    }
}
