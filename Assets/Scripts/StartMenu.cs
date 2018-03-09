using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public GameObject slider;
    public Canvas canvas;
    // Use this for initialization
    private void Awake()
    {
        slider.GetComponent<Slider>().value = GameController.volume;
        if (SceneManager.GetActiveScene().name != "MenuScene")
            canvas.GetComponent<Canvas>().enabled = false;
    }
    private void Update()
    {
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }
    public void ChangeDifficulty(int d)
    {
        GameController.difficulty = (Difficulty)d;
    }
    public void ChangeVolume(float v)
    {
        GameController.volume = v;
    }
}
