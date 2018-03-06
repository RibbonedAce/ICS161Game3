using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayThenDie : MonoBehaviour {

    void Awake ()
    {
        GetComponent<AudioSource>().volume = GameController.volume;
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
    private void Update()
    {
        GetComponent<AudioSource>().volume = GameController.volume;
    }
}
