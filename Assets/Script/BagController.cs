using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BagController : MonoBehaviour
{


    private Vector3 originalScale;
    public GameObject waterBottlesInBox;
    public GameObject energyBottlesInBox;

    [SerializeField] private Transform bag;
    public List<ProductData> productDataList;
    private Vector3 productSize;
    [SerializeField] TextMeshProUGUI maxText;
    int maxBagCapacity;
    // Start is called before the first frame update
    void Start()
    {
        TinySauce.OnGameStarted("Game Started");
        maxBagCapacity = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        /*
        if (other.CompareTag("ShopPoint"))
        {
            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                //Selling Products
                SellProductsToShop(productDataList[i]);
                Destroy(bag.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
                //Animation for Sold Items
                StartCoroutine(AddBottlesToBoxes());
            }

            ControlBagCapacity();


        }
        */
        if (other.CompareTag("UnlockedTrainArea"))
        {
            UnlockTrainUnit trainUnit = other.GetComponent<UnlockTrainUnit>();
            ProductType neededType = trainUnit.GetNeededProductType();

            // Start the coroutine to handle the product destruction in sequence
            StartCoroutine(DestroyProductsSequentially(trainUnit, neededType));

            StartCoroutine(PutProductsInOrder());
            ControlBagCapacity();


            //StartCoroutine(AddBottlesToBoxes());
            if (neededType == ProductType.water && productDataList.Count > 0)
            {
                StartCoroutine(AddWaterBottlesToBoxes());
            }
            if (neededType == ProductType.enegeryDrink && productDataList.Count > 0)
            {
                StartCoroutine(AddEnergyBottlesToBoxes());
            }
        }


    }
    private IEnumerator DestroyProductsSequentially(UnlockTrainUnit trainUnit, ProductType neededType)
    {
        List<GameObject> objectsToDestroy = new List<GameObject>();
        List<int> indicesToRemove = new List<int>();

        // Collect objects to be destroyed and their indices
        for (int i = productDataList.Count - 1; i >= 0; i--)
        {
            if (productDataList[i].productType == neededType)
            {
                if (trainUnit.StoreProduct() == true)
                {
                    objectsToDestroy.Add(bag.transform.GetChild(i).gameObject);
                    indicesToRemove.Add(i);
                }
            }
        }

        // Remove items one by one with delay
        for (int j = 0; j < objectsToDestroy.Count; j++)
        {
            PlayShopSound();
            // Animation for Sold Items


            // Destroy the child and remove the product data
            Destroy(objectsToDestroy[j]);

            // Remove product data from list
            productDataList.RemoveAt(indicesToRemove[j]);

            // Wait for a specified time before destroying the next item
            yield return new WaitForSeconds(0.1f); // Adjust the time as needed
        }
    }
    private void SellProductsToShop(ProductData productData)
    {
        //cashManager > product solded.
        CashManager.instance.ExchangeProduct(productData);

    }

    public void AddProductToBag(ProductData prodcutData)
    {

        GameObject bagProduct = Instantiate(prodcutData.productPrefab, Vector3.zero, Quaternion.identity);
        //Instantiate(object, position, rotation)
        bagProduct.transform.SetParent(bag, true);

        CalculateItemSize(bagProduct);
        float YPosition = CalculateNewYPositionOfItem();
        bagProduct.transform.localRotation = Quaternion.identity;
        bagProduct.transform.localPosition = Vector3.zero;
        bagProduct.transform.localPosition = new Vector3(0, YPosition, 0);
        productDataList.Add(prodcutData);
        ControlBagCapacity();
    }

    private float CalculateNewYPositionOfItem()
    {
        float newYPos = productSize.y * productDataList.Count;
        return newYPos;
    }
    private void CalculateItemSize(GameObject gameObject)
    {
        if (productSize == Vector3.zero)
        {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            productSize = renderer.bounds.size;
        }

    }
    private void ControlBagCapacity()
    {
        if (productDataList.Count == maxBagCapacity)
        {
            SetMaxTextOn();
            //Activate maxText
        }
        else
        {
            SetMaxTextOff();

        }
    }

    private void SetMaxTextOn()
    {
        if (!maxText.isActiveAndEnabled)
        {

            maxText.gameObject.SetActive(true);
            DOTween.Restart("MaxTextFade");
        }
    }
    private void SetMaxTextOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false);
        }
    }
    public bool IsEmptySpace()
    {
        if (productDataList.Count < maxBagCapacity)
        {
            return true;
        }
        return false;
    }

    IEnumerator AddWaterBottlesToBoxes()
    {

        waterBottlesInBox.SetActive(true);
        DOTween.Restart("WaterBottleBoxScaleOn");
        yield return new WaitForSeconds(6.0f);

        waterBottlesInBox.SetActive(false);

    }
    IEnumerator AddEnergyBottlesToBoxes()
    {

        energyBottlesInBox.SetActive(true);
        DOTween.Restart("EnergyBottleBoxScaleOn");
        yield return new WaitForSeconds(6.0f);

        energyBottlesInBox.SetActive(false);

    }

    private IEnumerator PutProductsInOrder()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < bag.childCount; i++)
        {
            float newYPos = productSize.y * i;
            bag.GetChild(i).transform.localPosition = new Vector3(0, newYPos, 0);
        }
    }
    private void PlayShopSound()
    {
        if (productDataList.Count > 0)
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
        }
    }
    private void PlayUnlockedSound()
    {
        AudioManager.instance.PlayAudio(AudioClipType.unlockedClip);
    }
}
