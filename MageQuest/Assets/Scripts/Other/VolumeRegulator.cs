using UnityEngine;

public class VolumeRegulator : MonoBehaviour
{
    private AudioSource _music;
    void Start()
    {
        _music = GetComponent<AudioSource>();
    }

    public void ChangeVolume(float volume) //получаем со слайдера
    {
        _music.volume = volume;
    }
}
