using System.Collections;
using UnityEngine;

public class ProductCollectController : MonoBehaviour
{
    private bool isReadyToPick;
    private Vector3 originalScale;
    [SerializeField] private ProductData productData;
    private BagController bagController;
    // Start is called before the first frame update
    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick)
        {
            bagController = other.GetComponent<BagController>();

            if (bagController.IsEmptySpace())
            {
                AudioManager.instance.PlayAudio(AudioClipType.grabClip);
                bagController.AddProductToBag(productData);

                isReadyToPick = false;
                StartCoroutine(ProductPicked());
            }


        }
    }
    IEnumerator ProductPicked()
    {
        Vector3 targetScale = Vector3.zero;
        transform.gameObject.LeanScale(targetScale, 0.5f);

        yield return new WaitForSeconds(3.0f);
        transform.gameObject.LeanScale(originalScale, 0.5f).setEase(LeanTweenType.easeOutBack);
        isReadyToPick = true;
    }




}
