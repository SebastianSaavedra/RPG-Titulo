using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent OnTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTriggerEnterEvent?.Invoke();

            if (gameObject.name == "AnchimallenTrigger")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
