using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Функция для кнопки "Начать"
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    // Функция для кнопки "Настройки"
    public void OpenSettings()
    {
        // Логика для открытия настроек
        Debug.Log("Открыть настройки");
    }

    // Функция для кнопки "Выход"
    public void ExitGame()
    {
        Application.Quit();
    }
}
