using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioSource[] sfx;
    [SerializeField] public AudioSource[] bgm;

    private void InitializeAudioManager() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(int sfxToPlay) {
        if(sfxToPlay < sfx.Length && sfxToPlay > -1) {
            sfx[sfxToPlay].Play();
        }
    }

    public void PlayBGM(int bgmToPlay) {
        if (!bgm[bgmToPlay].isPlaying) {
            StopMusic();
            if(bgmToPlay < bgm.Length && bgmToPlay > -1) {
                bgm[bgmToPlay].Play();
            }
        }
    }

    public void StopMusic() {
        for(int i = 0; i < bgm.Length; i++) {
            bgm[i].Stop();
        }
    }

    // Start is called before the first frame update
    void Start() {
        InitializeAudioManager();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            PlaySFX(5);
            PlayBGM(2);
        }
    }
}
