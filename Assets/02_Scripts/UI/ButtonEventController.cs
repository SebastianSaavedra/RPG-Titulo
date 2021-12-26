using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonEventController : MonoBehaviour, ISelectHandler,IDeselectHandler
{
    [SerializeField] GameObject imagen,icon;
    [SerializeField] GameObject text;
    [SerializeField] GameObject itemBtn;
    float velocidadDeAnimacion = .33f;
    private Vector3 baseRectTransformScale;
    private Vector3 baseRectTransformPosition;
    private Color color;

    [SerializeField] GameObject statsWindow;

    private void Awake()
    {
        if (imagen != null)
        {
            baseRectTransformScale = imagen.GetComponent<RectTransform>().localScale;
            baseRectTransformPosition = imagen.GetComponent<RectTransform>().localPosition;
            color.a = imagen.GetComponent<Image>().color.a;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        switch (eventData.selectedObject.name)
        {
            case "AttackMenu":
            case "ItemMenu":
            case "SpellsMenu":
            case "RunMenu":
                imagen.GetComponent<Image>().DOFade(color.a, velocidadDeAnimacion);
                imagen.transform.DOScale(baseRectTransformScale, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOAnchorPos(baseRectTransformPosition, velocidadDeAnimacion);
                icon.SetActive(false);
                break;
            case "Btn_Attack":
            case "Btn_Defense":
                imagen.GetComponent<Image>().DOFade(.25f, velocidadDeAnimacion);
                break;
            case "Heal":
            case "GroupHeal":
            case "Revive":
            case "ExtraTurns":
                text.SetActive(false);
                break;
            case "Top":
            case "Mid":
            case "Bottom":
                if (statsWindow.activeInHierarchy)
                {
                    statsWindow.SetActive(false);
                }
                break;
            case "Btn_Consumibles":
            case "Btn_Equipamiento":
                imagen.GetComponent<RectTransform>().DOLocalMoveX(551f, 1f);
                break;
            case "CharacterStats":
                imagen.SetActive(false);
                break;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        switch (eventData.selectedObject.name)
        {
            case "AttackMenu":
                imagen.GetComponent<Image>().DOFade(1f,velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOScale(1.5f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOAnchorPos(new Vector3(1.9f, -94.5f, 0), velocidadDeAnimacion);
                icon.SetActive(true);
                break;
            case "ItemMenu":
                imagen.GetComponent<Image>().DOFade(1f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOScale(1.5f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-59.4f, -59, 0), velocidadDeAnimacion);
                icon.SetActive(true);
                break;
            case "SpellsMenu":
                imagen.GetComponent<Image>().DOFade(1f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOScale(1.5f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-56, -55, 0), velocidadDeAnimacion);
                icon.SetActive(true);
                break;
            case "RunMenu":
                imagen.GetComponent<Image>().DOFade(1f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOScale(1.5f, velocidadDeAnimacion);
                imagen.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-48, -50, 0), velocidadDeAnimacion);
                icon.SetActive(true);
                break;
            case "Btn_Attack":
            case "Btn_Defense":
                imagen.GetComponent<Image>().DOFade(1f, velocidadDeAnimacion);
                break;
            case "Heal":
            case "GroupHeal":
            case "Revive":
            case "ExtraTurns":
                text.SetActive(true);
                break;
            case "Btn_Consumibles":
            case "Btn_Equipamiento":
                imagen.GetComponent<RectTransform>().DOLocalMoveX(616.32f, 1f);
                break;
            case "CharacterStats":
                imagen.SetActive(true);
                break;
        }
    }
}
