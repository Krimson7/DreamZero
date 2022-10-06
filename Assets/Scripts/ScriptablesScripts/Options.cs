using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Options", menuName = "DreamZero/Option", order = 5)]
public class Options : ScriptableObject
{
    public float Volume;

    public bool paused = false;

    public AudioClip[] BGMs;

    public List<I_AudioSourceListener> AudioListeners = new List<I_AudioSourceListener>();

    public void setVolume(float volume){
        Volume = volume;
        onChangeVolume();
    }

    public void AddAudioListener(I_AudioSourceListener listener){
        AudioListeners.Add(listener);
    }

    public void RemoveAudioListener(I_AudioSourceListener listener){
        AudioListeners.Remove(listener);
    }

    public void onChangeVolume(){
        foreach (I_AudioSourceListener AS in AudioListeners)
        {
            AS.OnVolumeChanged();
        }
    }

    public void changeBGM(int index){
        foreach (I_AudioSourceListener AS in AudioListeners)
        {
            // AS.OnBGMChanged(BGMs[index]);
        }
    }

    private void OnEnable() {
        paused = false;
    }

    public void pause(){
        if(paused){
            SceneManager.UnloadSceneAsync("PauseMenu");
            // Debug.Log("unpause");
            paused = false;
        }else{
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            // Debug.Log("pause");
            paused = true;
        }
    }

    public void unPause(){
        //force unpause
        SceneManager.UnloadSceneAsync("PauseMenu");
        paused = false;
        
    }
}
