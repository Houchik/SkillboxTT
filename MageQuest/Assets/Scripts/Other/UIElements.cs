using UnityEngine;

public class UIElements : MonoBehaviour
{
    private Fade _fade;

    private void Start()
    {
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
    }

    public void Play()
    {
        Time.timeScale = 1;
        _fade.FadeIn("GameScene"); //запустить затемнение экрана и загрузить сцену
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        _fade.FadeIn("MainMenuScene"); //запустить затемнение экрана и загрузить сцену
    }

    public void Exit()
    {
        Application.Quit();
    }
}
