using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // ������� ��� ������ "������"
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    // ������� ��� ������ "���������"
    public void OpenSettings()
    {
        // ������ ��� �������� ��������
        Debug.Log("������� ���������");
    }

    // ������� ��� ������ "�����"
    public void ExitGame()
    {
        Application.Quit();
    }
}
