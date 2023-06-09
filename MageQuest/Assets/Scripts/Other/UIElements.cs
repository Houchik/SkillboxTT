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
        _fade.FadeIn("GameScene"); //��������� ���������� ������ � ��������� �����
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        _fade.FadeIn("MainMenuScene"); //��������� ���������� ������ � ��������� �����
    }

    public void Exit()
    {
        Application.Quit();
    }
}
