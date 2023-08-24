using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volume : MonoBehaviour
{
    public AudioMixer Mixer;

    public void SetLevel (float slidevalue)
    {
        Mixer.SetFloat("Sound", Mathf.Log10(slidevalue) * 20);
    }


}
