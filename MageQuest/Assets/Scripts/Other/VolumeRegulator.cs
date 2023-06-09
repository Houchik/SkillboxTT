using UnityEngine;

public class VolumeRegulator : MonoBehaviour
{
    private AudioSource _music;
    void Start()
    {
        _music = GetComponent<AudioSource>();
    }

    public void ChangeVolume(float volume) //�������� �� ��������
    {
        _music.volume = volume;
    }
}
