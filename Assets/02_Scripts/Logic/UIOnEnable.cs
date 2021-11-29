using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.EventSystems;

public class UIOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(MenuManager.instance.menus[0]);
        }
        Timing.RunCoroutine(MenuManager.instance._EventSystemReAssign());
    }
}
