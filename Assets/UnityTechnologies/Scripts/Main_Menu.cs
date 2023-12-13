using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject Panel;

    private void Start()
    {
        Close();
    }

    public void Start_Game()
    {
        SceneManager.LoadScene(1);
    }
    public void Info()
    {
        Panel.SetActive(true);
            }
    public void Quit()
    {
        Application.Quit();

    }
    public void Close()
    {
        Panel.SetActive(false);

    }
}