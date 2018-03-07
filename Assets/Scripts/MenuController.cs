﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {
    public GameObject slider;
    public GameObject canvas;
    public static bool isGamePaused;
    public Image WinImage;
    public List<Text> text;
    // Use this for initialization
    private void Awake()
    {
        slider.GetComponent<Slider>().value = GameController.volume;
        if(SceneManager.GetActiveScene().name != "MenuScene")
            canvas.GetComponent<Canvas>().enabled = false;
        isGamePaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MenuScene")
        {
            if (canvas.GetComponent<Canvas>().enabled == false)
            {
                Time.timeScale = 0;
                canvas.GetComponent<Canvas>().enabled = true;
                isGamePaused = true;
            }
            else Resume();
        }
        if(GameController.status == GameStatus.Won)
        {
            text[0].text = "X " + GameController.KillCountEnemy1.ToString();
            text[1].text = "X " + GameController.KillCountEnemy2.ToString();
            text[2].text = "X " + GameController.KillCountEnemy3.ToString();
            WinImage.enabled = true;
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
        canvas.GetComponent<Canvas>().enabled = false;
        isGamePaused = false;
        Time.timeScale = 1;
    }
}