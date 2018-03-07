using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {
    public static MenuController instance;
    public GameObject slider;
    public List<Canvas> canvas;
    public static bool isGamePaused;
    public GameObject WinImage;
    public GameObject LostImage;
    public List<Text> text;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
        slider.GetComponent<Slider>().value = GameController.volume;
        if(SceneManager.GetActiveScene().name != "MenuScene")
            canvas[0].GetComponent<Canvas>().enabled = false;
        isGamePaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MenuScene")
        {
            if (canvas[0].GetComponent<Canvas>().enabled == false)
            {
                Time.timeScale = 0;
                canvas[0].GetComponent<Canvas>().enabled = true;
                isGamePaused = true;
            }
            else Resume();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void ChangeDifficulty(int d)
    {
        GameController.difficulty = (Difficulty)d;
    }
    public void ChangeVolume(float v)
    {
        GameController.volume = v;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void Resume()
    {
        canvas[0].GetComponent<Canvas>().enabled = false;
        isGamePaused = false;
        Time.timeScale = 1;
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GameWon()
    {
        GameController.status = GameStatus.Won;
        WinImage.SetActive(true);
        text[0].text = "X " + GameController.KillCountEnemy[0].ToString();
        text[1].text = "X " + GameController.KillCountEnemy[1].ToString();
        text[2].text = "X " + GameController.KillCountEnemy[2].ToString();
        if (SceneManager.GetActiveScene().buildIndex < 3)
            Invoke("ChangeScene", 3.5f);
        else
            Invoke("MainMenu", 3.5f);
    }
    public void GameLost()
    {
        GameController.status = GameStatus.Lost;
        LostImage.SetActive(true);
        Invoke("RestartLevel",3.0f);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
