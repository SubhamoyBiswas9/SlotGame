using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image middleItem;

    [SerializeField] Sprite[] sprites;

    public static event System.Action<Sprite> OnComplete;

    int randVal;

    Sprite originalSprite;

    private void OnEnable()
    {
        SlotManager.OnSpinEvent += SlotManager_OnSpinEvent;
    }

    private void OnDisable()
    {
        SlotManager.OnSpinEvent -= SlotManager_OnSpinEvent;
    }

    private void SlotManager_OnSpinEvent(int randVal)
    {
        if (originalSprite != null)
            middleItem.sprite = originalSprite;

        StartCoroutine(Rotate());
        this.randVal = randVal;
    }

    IEnumerator Rotate()
    {
        float timeInterval = .025f;

        for(int i = 0; i < 120; i++)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y + 680f/6f);

            if (GetComponent<RectTransform>().anchoredPosition.y >= 340f)
                GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, -340f);

            yield return new WaitForSeconds(timeInterval);
        }

        if (randVal != 0)
        {
            originalSprite = middleItem.sprite;
            middleItem.sprite = sprites[Random.Range(0, sprites.Length)];
        }
            

        OnComplete?.Invoke(middleItem.sprite);
    }
}
