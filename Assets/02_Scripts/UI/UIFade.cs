using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFade : MonoBehaviour
{
    private static UIFade instance;
    private float timer = 1f;
    [SerializeField] private GameObject continuara;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        //Hide();
    }

    public static void Hide()
    {
        instance.gameObject.SetActive(false);
    }

    public static void Show()
    {
        instance.gameObject.SetActive(true);
    }

    public static void FadeIn()
    {
        instance.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        instance.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f);
    }

    public static void FadeOut()
    {
        instance.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        instance.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 1f);
    }

    public static void Continuara()
    {
        instance.continuara.SetActive(true);
    }

    public static float GetTimer()
    {
        return instance.timer;
    }
}
