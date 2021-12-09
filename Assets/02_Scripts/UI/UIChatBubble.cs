using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChatBubble : MonoBehaviour 
{

    private static UIChatBubble instance;

    [Range(0.01f,0.1f)]
    public float textSpeed = .03f;

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
        Text_Writer.AddWriter(uiText, text, textSpeed, true,true);
        SoundManager.PlaySound(SoundManager.Sound.Talking, text.Length * textSpeed);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

}
