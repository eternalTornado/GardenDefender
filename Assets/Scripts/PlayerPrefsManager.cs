using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string VOLUME_CODE = "volume";
    public static void SetVolume(float volume)
    {
        if(volume < 0 || volume > 1)
        {
            Debug.LogError("Volume cannot be either less than 0 or greater than 1");
            return;
        }
        PlayerPrefs.SetFloat(VOLUME_CODE, volume);
    }
    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME_CODE);
    }
}
