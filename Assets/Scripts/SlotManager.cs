using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    List<Sprite> finalSprites = new List<Sprite>();

    int slotCount;

    public static event Action OnSpinEvent;

    [SerializeField] TMP_Text resultText;

    [SerializeField] Button spinBtn;

    private void OnEnable()
    {
        Slot.OnComplete += Slot_OnComplete;
    }

    private void OnDisable()
    {
        Slot.OnComplete -= Slot_OnComplete;
    }

    private void Start()
    {
        slotCount = FindObjectsByType<Slot>(FindObjectsSortMode.None).Length;
    }

    public void Spin()
    {
        resultText.gameObject.SetActive(false);
        if (finalSprites.Count > 0)
            finalSprites.Clear();

        OnSpinEvent?.Invoke();
        spinBtn.interactable = false;
    }

    private void Slot_OnComplete(Sprite sprite)
    {
        finalSprites.Add(sprite);

        if(finalSprites.Count == slotCount)
        {
            bool allEqual = true;
            for (int i = 1; i < finalSprites.Count; i++)
            {
                if (finalSprites[i] != finalSprites[0])
                {
                    allEqual = false;
                    break;
                }
            }

            resultText.gameObject.SetActive(true);

            if (allEqual)
            {
                resultText.text = "Yaay! You Win! Congratulations!";
            }
            else
            {
                resultText.text = "Better luck next time";
            }

            spinBtn.interactable = true;
        }
    }
}
