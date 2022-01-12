using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class DialogueController : MonoBehaviour
{

    private static DialogueController instance;
    public static DialogueController GetInstance() => instance;

    private Transform leftCharacterTransform;
    private Transform leftCharacterNameplateTransform;
    private Transform rightCharacterNameplateTransform;
    private Transform rightCharacterTransform;
    private UIChatBubble chatBubble;
    private TextMeshProUGUI leftCharacterNameText;
    private TextMeshProUGUI rightCharacterNameText;
    private List<Action> actionList;
    private List<DialogueOption> dialogOptionList;


    protected Transform leftCharacterPosition, rightCharacterPosition, leftCharacterNamePosition, rightCharacterNamePosition, leftCharacterNameplatePosition,rightCharacterNameplatePosition;


    private void Awake() 
    {
        instance = this;

        leftCharacterTransform = transform.Find("LeftCharacter");
        leftCharacterPosition = leftCharacterTransform;
        rightCharacterTransform = transform.Find("RightCharacter");
        rightCharacterPosition = rightCharacterTransform;

        chatBubble = transform.Find("ChatBubbleUI").GetComponent<UIChatBubble>();

        leftCharacterNameText = transform.Find("LeftCharacterName").GetComponent<TextMeshProUGUI>();
        leftCharacterNamePosition = leftCharacterNameText.transform;
        rightCharacterNameText = transform.Find("RightCharacterName").GetComponent<TextMeshProUGUI>();
        rightCharacterNamePosition = rightCharacterNameText.transform;

        leftCharacterNameplateTransform = transform.Find("LeftNameplate");
        leftCharacterNameplatePosition = leftCharacterNameplateTransform;
        rightCharacterNameplateTransform = transform.Find("RightNameplate");
        rightCharacterNameplatePosition = rightCharacterNameplateTransform;

        ShowLeftCharacterName("");
        ShowRightCharacterName("");

        //chatBubble.Hide();

        HideLeftCharacter();
        HideRightCharacter();
        HideLeftNameplate();
        HideRightNameplate();

        Hide();
    }

    private void Update() {
        if (dialogOptionList == null && TestInput()) 
        {
            //SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            PlayNextAction();
        }
    }

    private bool TestInput() 
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public void ShowDialogueOptions(List<DialogueOption> dialogOptionList) 
    {
        this.dialogOptionList = dialogOptionList;
        foreach (DialogueOption dialogOption in dialogOptionList) 
        {
            dialogOption.CreateTransform(transform);
        }
    }

    public void ClearDialogueOptions() {
        if (dialogOptionList == null) return;
        foreach (DialogueOption dialogOption in dialogOptionList) 
        {
            dialogOption.DestroySelf();
        }
        dialogOptionList.Clear();
        dialogOptionList = null;
    }

    public void SetDialogueActions(List<Action> actionList, bool autoPlayNextAction) 
    {
        this.actionList = actionList;
        if (autoPlayNextAction) 
        {
            PlayNextAction();
        }
    }

    public void PlayNextAction() 
    {
        Action action = actionList[0];
        actionList.RemoveAt(0);
        action();
    }

    public SuperTextMesh GetSuperTextMesh()
    {
        return chatBubble.GetSuperTextMesh();
    }

    public void ShowLeftCharacterName(string name) 
    {
        leftCharacterNameText.text = name;
    }

    public void ShowRightCharacterName(string name)
    {
        rightCharacterNameText.text = name;
    }
    public void ShowRightCharacterName(string name,Vector3 position)
    {
        rightCharacterNameText.transform.localPosition = position;
        rightCharacterNameText.text = name;
    }

    public void HideLeftCharacterName() 
    {
        ShowLeftCharacterName("");
    }

    public void HideRightCharacterName() 
    {
        ShowRightCharacterName("");
    }

    public void ShowText(string text)
    {
        chatBubble.ShowText(text);
    }

    public void ShowLeftNameplate()
    {
        leftCharacterNameplateTransform.gameObject.SetActive(true);
    }
    public void ShowRightNameplate()
    {
        rightCharacterNameplateTransform.gameObject.SetActive(true);
    }
    public void ShowRightNameplate(Vector3 position)
    {
        rightCharacterNameplateTransform.localPosition = position;
        rightCharacterNameplateTransform.gameObject.SetActive(true);
    }

    public void HideLeftNameplate()
    {
        leftCharacterNameplateTransform.gameObject.SetActive(false);
    }
    public void HideRightNameplate()
    {
        rightCharacterNameplateTransform.gameObject.SetActive(false);
    }

    //public void HideText() {
    //    chatBubble.Hide();
    //}

    public void ShowLeftActiveTalkerHideRight(string text) {
        FadeRightCharacter();
        //HideText();
        UnFadeLeftCharacter();
        ShowText(text);
    }

    public void ShowRightActiveTalkerHideLeft(string text) {
        FadeLeftCharacter();
        //HideText();
        UnFadeRightCharacter();
        ShowText(text);
    }

    public void ShowLeftCharacter(Sprite characterSprite, bool faded) {
        leftCharacterTransform.gameObject.SetActive(true);
        leftCharacterTransform.GetComponent<Image>().sprite = characterSprite;
        leftCharacterTransform.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        if (faded) {
            leftCharacterTransform.GetComponent<Image>().color = new Color(.4f, .4f, .4f, 1f);
        }
    }

    public void FadeLeftCharacter() {
        leftCharacterTransform.GetComponent<Image>().color = new Color(.4f, .4f, .4f, 1f);
    }

    public void UnFadeLeftCharacter() {
        leftCharacterTransform.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
    }

    public void HideLeftCharacter() {
        leftCharacterTransform.gameObject.SetActive(false);
    }

    public void ShowRightCharacter(Sprite characterSprite, bool faded)
    {
        rightCharacterTransform.gameObject.SetActive(true);
        rightCharacterTransform.GetComponent<Image>().sprite = characterSprite;
        rightCharacterTransform.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        if (faded)
        {
            rightCharacterTransform.GetComponent<Image>().color = new Color(.4f, .4f, .4f, 1f);
        }
    }

    public void ShowRightCharacter(Sprite characterSprite, bool faded, Vector3 position)
    {
        rightCharacterTransform.gameObject.SetActive(true);
        rightCharacterTransform.localPosition = position;
        rightCharacterTransform.GetComponent<Image>().sprite = characterSprite;
        rightCharacterTransform.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        if (faded)
        {
            rightCharacterTransform.GetComponent<Image>().color = new Color(.4f, .4f, .4f, 1f);
        }
    }

    public void FadeRightCharacter() {
        rightCharacterTransform.GetComponent<Image>().color = new Color(.4f, .4f, .4f, 1f);
    }

    public void UnFadeRightCharacter() {
        rightCharacterTransform.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
    }

    public void HideRightCharacter() {
        rightCharacterTransform.gameObject.SetActive(false);
    }

    public void Hide() {
        gameObject.SetActive(false);
        leftCharacterTransform.localPosition = leftCharacterPosition.localPosition;
        rightCharacterTransform.localPosition = rightCharacterPosition.localPosition;
        leftCharacterNameplateTransform.localPosition = leftCharacterNameplatePosition.localPosition;
        rightCharacterNameplateTransform.localPosition = rightCharacterNameplatePosition.localPosition;
        leftCharacterNameText.transform.localPosition = leftCharacterNamePosition.localPosition;
        rightCharacterNameText.transform.localPosition = rightCharacterNamePosition.localPosition;
    }

    public void Show() {
        gameObject.SetActive(true);
        GetSuperTextMesh().readDelay = .03f;
    }






    /*
     * Single Dialogue Option
     * */
    public class DialogueOption {

        private Transform transform;
        private string text;
        private Action triggerAction;
        private Option option;

        public enum Option {
            _1,
            _2,
            _3,
        }

        public DialogueOption(Option option, string text, Action triggerAction) {
            this.option = option;
            this.text = text;
            this.triggerAction = triggerAction;
        }

        public void CreateTransform(Transform parent) {
            transform = Instantiate(GameAssets.i.pfChatOption, parent);
            Vector2 anchoredPosition;
            switch (option) {
            default:
            case Option._1: anchoredPosition = new Vector2(320, 100); break;
            case Option._2: anchoredPosition = new Vector2(320, 50);  break;
            case Option._3: anchoredPosition = new Vector2(320, 150); break;
            }
            transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            transform.Find("text").GetComponent<Text>().text = text;
            transform.GetComponent<Button_UI>().ClickFunc = Trigger;
        }

        public void Trigger() {
            triggerAction();
        }

        public void DestroySelf() {
            Destroy(transform.gameObject);
        }

    }

}
