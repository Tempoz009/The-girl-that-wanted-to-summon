using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public void PlayMainGame()
    {
        Time.timeScale = 1f;
        enemy.dmg = 0.5f;
        playerScript.playerHP = 5;
        playerScript.movSpeed = 20;
        playerScript.currentWave = 0;
        Shoot.timeBetweenShots = 10;
        playerScript.EnemyKilled = 0;
        SceneManager.LoadScene(1);

    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
  
    public void CloseGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(text1 != null && text2 != null)
        {
            text1.text = $"Waves passed: {playerScript.currentWave.ToString()}";
            text2.text = $"Monsters killed: {playerScript.EnemyKilled.ToString()}";
        }
    }
}
