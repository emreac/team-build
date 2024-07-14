using TMPro;
using UnityEngine;

public class UnlockTrainUnit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI drinkText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;
    private int storedProductCount;

    [SerializeField] private int useProductInSeconds = 10;
    [SerializeField] private Transform coinTransfom;
    [SerializeField] private GameObject coinGO;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        DisplayProductCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (storedProductCount > 0)
        {
            time += Time.deltaTime;

            if (time >= useProductInSeconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
    }

    private void DisplayProductCount()
    {
        drinkText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }
    public bool StoreProduct()
    {
        if (maxStoredProductCount == storedProductCount)
        {
            return true;
        }
        storedProductCount++;
        DisplayProductCount();
        return true;
    }

    private void UseProduct()
    {
        storedProductCount--;
        DisplayProductCount();
        CreateCoin();
    }
    private void CreateCoin()
    {
        Vector3 position = Random.insideUnitSphere * 1f;
        Vector3 InstantiatePos = coinTransfom.position + position;
        Instantiate(coinGO, InstantiatePos, Quaternion.identity);
    }
}
