using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchimallenOWController : MonoBehaviour
{
    [SerializeField] Transform[] posArray;
    [SerializeField] float speed; 
    int index = 0;

    bool triggered;

    private void Update()
    {
        if (triggered)
        {
            transform.position = Vector2.MoveTowards(transform.position, posArray[index].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, posArray[index].position) < .01f)
            {
                if (index < posArray.Length - 1)
                {
                    index++;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetTriggered()
    {
        triggered = true;
    }
}
