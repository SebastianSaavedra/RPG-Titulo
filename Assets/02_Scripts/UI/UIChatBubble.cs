using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChatBubble : MonoBehaviour 
{

    private static UIChatBubble instance;

    public static void Create(Vector2 position, string text) 
    {
        Transform chatBubbleUITransform = Instantiate(GameAssets.i.pfChatBubbleUI, instance.transform);
    }

    private TextMeshProUGUI uiText;

    private void Awake() 
    {
        instance = this;
        uiText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void ShowText(string text)
    {
        Show();
        uiText.text = "";
        Text_Writer.RemoveWriter(uiText);
        Text_Writer.AddWriter(uiText, text, .02f, true);
        SoundManager.PlaySound(SoundManager.Sound.Talking, text.Length * .02f);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

}
