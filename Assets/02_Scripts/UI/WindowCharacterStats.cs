using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class WindowCharacterStats : MonoBehaviour
{
    [SerializeField] PartyUIController partyUIController;
    [SerializeField] TextMeshProUGUI vida, ataque, defensa, recursos, turnos;//,experiencia,nivel;
    [SerializeField] Image characterSplashArt;

    private void OnEnable()
    {
        if (partyUIController.pjActual != null)
        {
            switch (partyUIController.pjActual.type)
            {
                case Character.Type.Suyai:
                    characterSplashArt.sprite = GameAssets.i.splashSuyai;
                    recursos.SetText("Hierbas: {0}", ResourceManager.instance.GetHerbsAmount());
                    break;
                case Character.Type.Pedro:
                    characterSplashArt.sprite = GameAssets.i.splashPedro;
                    recursos.SetText("Monedas: {0}", ResourceManager.instance.GetMoneyAmount());
                    break;
                case Character.Type.Chillpila:
                    characterSplashArt.sprite = GameAssets.i.splashChillpila;
                    recursos.SetText("Almas: {0}", ResourceManager.instance.GetSoulsAmount());
                    break;
                case Character.Type.Arana:
                    characterSplashArt.sprite = GameAssets.i.splashArana;
                    recursos.SetText("Tatuajes: {0}", ResourceManager.instance.GetTattoosAmount());
                    break;
                case Character.Type.Antay:
                    characterSplashArt.sprite = GameAssets.i.splashAntay;
                    recursos.SetText("Golpes: {0}", ResourceManager.instance.GetHitsAmount());
                    break;
            }
            vida.SetText("Puntos de Vida: {0} / {1} ", partyUIController.pjActual.stats.health, partyUIController.pjActual.stats.healthMax);
            ataque.SetText("Ataque: {0}", partyUIController.pjActual.stats.attack);
            defensa.SetText("Defensa: {0}", partyUIController.pjActual.stats.defense);
            turnos.SetText("Turnos: " + partyUIController.pjActual.stats.turns);
            //experiencia.SetText("Ataque: {0}", menuStateController.pjActual.stats.attack);
            //nivel.SetText("Ataque: {0}", menuStateController.pjActual.stats.attack);
        }
    }
}
