using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private Animator _animator;

    private string _sceneToLoad;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnLevelWasLoaded(int level) //����������� ������
    {
        _animator.Play("FadeOut");   
    }

    public void FadeIn(string sceneName) //���������� ������
    {
        Debug.Log(0);
        _animator.Play("FadeIn");
        _sceneToLoad = sceneName;
    }

    public void FadeInEnd() //����� ��������
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
