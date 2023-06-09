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

    private void OnLevelWasLoaded(int level) //растемнение экрана
    {
        _animator.Play("FadeOut");   
    }

    public void FadeIn(string sceneName) //затемнение экрана
    {
        Debug.Log(0);
        _animator.Play("FadeIn");
        _sceneToLoad = sceneName;
    }

    public void FadeInEnd() //конец анимации
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
