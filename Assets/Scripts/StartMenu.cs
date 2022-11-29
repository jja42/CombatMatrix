using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject InfoUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInfo()
    {
        InfoUI.SetActive(true);
    }

    public void CloseInfo()
    {
        InfoUI.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
