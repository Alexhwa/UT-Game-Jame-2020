using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAudio : MonoBehaviour
{
    public AudioSource RoomBgSource;
    public AudioClip RoomMusic;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayLoop(RoomBgSource, RoomMusic, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
