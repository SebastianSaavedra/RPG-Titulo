using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimsSFX : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource sfx;

    void Start()
    {
        sfx = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SFX()
    {
        sfx.Play();
    }

}
