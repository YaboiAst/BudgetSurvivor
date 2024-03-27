using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject deathMenu;

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI deathMenuScoreText;

    [SerializeField] private GameObject errorOnInputField;
    private bool validName;
    
    private float timeSurvived;
    private bool isGameRunning;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void StartGame()
    {
        timeSurvived = 0f;
        if (!validName)
        {
            ShowError();
            return;
        }
        
        mainMenu.SetActive(false);
        deathMenu.SetActive(false);
        spawner.SetActive(true);
        player.SetActive(true);
        isGameRunning = true;
    }

    private void Update()
    {
        if(isGameRunning)
            timeSurvived += Time.deltaTime;
    }


    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        spawner.SetActive(false);
        player.SetActive(false);
        deathMenu.SetActive(false);
    }
    
    public void ShowDeathMenu(string playerName)
    {
        isGameRunning = false;
        // Show time survived with only 2 decimal places
        deathMenuScoreText.text = playerName + " sobreviveu por " + timeSurvived.ToString("F2") + " segundos";
        deathMenu.SetActive(true);
        spawner.SetActive(false);
        player.SetActive(false);
        mainMenu.SetActive(false);
    }
    
    public void CheckIfValidName(TMP_InputField inputField)
    {
        if(inputField.text.Length is < 3 or > 10)
        {
            validName = false;
            ShowError();
        }
        else
        {
            playerName.text = inputField.text;
            validName = true;
        }
    }

    private void ShowError()
    {
        CancelInvoke(nameof(DisableError));
        errorOnInputField.SetActive(true);
        Invoke(nameof(DisableError), 4f);
    }
    
    private void DisableError()
    {
        errorOnInputField.SetActive(false);
    }
    
    
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
