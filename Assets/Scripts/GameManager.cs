using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private TextMeshProUGUI playerName;

    [SerializeField] private GameObject errorOnInputField;
    private bool validName;
    public void StartGame()
    {
        if (!validName)
        {
            ShowError();
            return;
        }
        
        mainMenu.SetActive(false);
        spawner.SetActive(true);
        player.SetActive(true);
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
