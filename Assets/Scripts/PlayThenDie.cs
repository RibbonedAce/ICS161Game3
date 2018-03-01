using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayThenDie : MonoBehaviour {

    void Awake ()
    {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
