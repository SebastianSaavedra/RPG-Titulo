using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoMenuWindow : MonoBehaviour
{
    public static InfoMenuWindow instance;

    public CharacterWindows[] characterWindowArray;

    [System.Serializable]
    public class CharacterWindows
    {
        public GameObject window;
        public Image icon;
        public TextMeshProUGUI nombre, vida, recurso;
    }
    string herbsText = "Hierbas", moneyText = "Dinero", soulsText = "Almas", hitsText = "Golpes", tattooText = "Tattoos";



    private void Awake()        //Si a futuro ocurren problemas quizas sea neceasario cambiarlo a start y mandar una corrutina
    {
        instance = this;
        ResourceManager.instance.OnMoneyChanged += ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnHerbsChanged += ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnSoulsChanged += ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnTattoosChanged += ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnHitsChanged += ResourceManager_OnResourceChanged;
    }
    void Start()
    {
        foreach (Character character in GameData.characterList)
        {
            if (character.IsInPlayerTeam())
            {
                if (character.lanePosition == Character.LanePosition.Up)
                {
                    characterWindowArray[0].nombre.text = character.name;
                    characterWindowArray[0].vida.SetText("PV: {0} / {1}", character.stats.health, character.stats.healthMax);
                    ChooseIconAndResource(character,0);
                }
                else if (character.lanePosition == Character.LanePosition.Middle)
                {
                    characterWindowArray[1].nombre.text = character.name;
                    characterWindowArray[1].vida.SetText("PV: {0} / {1}", character.stats.health, character.stats.healthMax);
                    ChooseIconAndResource(character,1);
                }
                else if (character.lanePosition == Character.LanePosition.Down)
                {
                    characterWindowArray[2].nombre.text = character.name;
                    characterWindowArray[2].vida.SetText("PV: {0} / {1}", character.stats.health, character.stats.healthMax);
                    ChooseIconAndResource(character,2);
                }
            }
        }
    }
    void ChooseIconAndResource(Character character, int x)
    {
        switch (character.type)
        {
            case Character.Type.Suyai:
                characterWindowArray[x].icon.sprite = GameAssets.i.splashSuyai;
                characterWindowArray[x].recurso.SetText(herbsText + ": {0}", ResourceManager.instance.GetHerbsAmount());

                break;
            case Character.Type.Antay:
                characterWindowArray[x].icon.sprite = GameAssets.i.splashAntay;
                characterWindowArray[x].recurso.SetText(hitsText + ": {0}", ResourceManager.instance.GetHitsAmount());

                break;
            case Character.Type.Pedro:
                characterWindowArray[x].icon.sprite = GameAssets.i.splashPedro;
                characterWindowArray[x].recurso.SetText(moneyText + ": {0}", ResourceManager.instance.GetMoneyAmount());

                break;
            case Character.Type.Chillpila:
                characterWindowArray[x].icon.sprite = GameAssets.i.splashChillpila;
                characterWindowArray[x].recurso.SetText(soulsText + ": {0}", ResourceManager.instance.GetSoulsAmount());

                break;
            case Character.Type.Arana:
                characterWindowArray[x].icon.sprite = GameAssets.i.splashArana;
                characterWindowArray[x].recurso.SetText(tattooText + ": {0}", ResourceManager.instance.GetTattoosAmount());
                break;

        }
    }
    void UpdateResourceUI(Character character, int x)
    {
        switch (character.type)
        {
            case Character.Type.Suyai:
                characterWindowArray[x].recurso.SetText(herbsText + ": {0}", ResourceManager.instance.GetHerbsAmount());

                break;
            case Character.Type.Antay:
                characterWindowArray[x].recurso.SetText(hitsText + ": {0}", ResourceManager.instance.GetHitsAmount());

                break;
            case Character.Type.Pedro:
                characterWindowArray[x].recurso.SetText(moneyText + ": {0}", ResourceManager.instance.GetMoneyAmount());

                break;
            case Character.Type.Chillpila:
                characterWindowArray[x].recurso.SetText(soulsText + ": {0}", ResourceManager.instance.GetSoulsAmount());

                break;
            case Character.Type.Arana:
                characterWindowArray[x].recurso.SetText(tattooText + ": {0}", ResourceManager.instance.GetTattoosAmount());
                break;

        }
    }

    private void ResourceManager_OnResourceChanged(object sender, System.EventArgs e)
    {
        foreach (Character character in GameData.characterList)
        {
            if (character.IsInPlayerTeam())
            {
                if (character.lanePosition == Character.LanePosition.Up)
                {
                    characterWindowArray[0].nombre.text = character.name;
                    UpdateResourceUI(character,0);
                }
                else if (character.lanePosition == Character.LanePosition.Middle)
                {
                    characterWindowArray[1].nombre.text = character.name;
                    UpdateResourceUI(character,1);
                }
                else if (character.lanePosition == Character.LanePosition.Down)
                {
                    characterWindowArray[2].nombre.text = character.name;
                    UpdateResourceUI(character,2);
                }
            }
        }
    }
    public void OnHealthChanged(Character character,HealthSystem healthSystem)
    {
        if (character.lanePosition == Character.LanePosition.Up)
        {
            UpdateHealthUI(character, healthSystem,0);
        }
        else if (character.lanePosition == Character.LanePosition.Middle)
        {
            UpdateHealthUI(character, healthSystem,1);
        }
        else if (character.lanePosition == Character.LanePosition.Down)
        {
            UpdateHealthUI(character, healthSystem,2);
        }
    }
    void UpdateHealthUI(Character character, HealthSystem healthSystem, int x)
    {
        switch (character.type)
        {
            case Character.Type.Suyai:
                characterWindowArray[x].vida.SetText("PV: {0} / {1}", healthSystem.GetHealthAmount(), healthSystem.GetMaxHealthAmount());

                break;
            case Character.Type.Antay:
                characterWindowArray[x].vida.SetText("PV: {0} / {1}", healthSystem.GetHealthAmount(), healthSystem.GetMaxHealthAmount());

                break;
            case Character.Type.Pedro:
                characterWindowArray[x].vida.SetText("PV: {0} / {1}", healthSystem.GetHealthAmount(), healthSystem.GetMaxHealthAmount());

                break;
            case Character.Type.Chillpila:
                characterWindowArray[x].vida.SetText("PV: {0} / {1}", healthSystem.GetHealthAmount(), healthSystem.GetMaxHealthAmount());

                break;
            case Character.Type.Arana:
                characterWindowArray[x].vida.SetText("PV: {0} / {1}", healthSystem.GetHealthAmount(), healthSystem.GetMaxHealthAmount());
                break;

        }
    }

    private void OnDestroy()
    {
        ResourceManager.instance.OnMoneyChanged -= ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnHerbsChanged -= ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnSoulsChanged -= ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnTattoosChanged -= ResourceManager_OnResourceChanged;
        ResourceManager.instance.OnHitsChanged -= ResourceManager_OnResourceChanged;
    }
}
