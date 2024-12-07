using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_Text errorText;


    // ENCAPSULATION
    String playerName
    {
        get { return nameInputField.text; }
        set
        {
            if (value.Length > 5)
            {
                Debug.LogError("Player name can't be  longer than five characters");
                errorText.gameObject.SetActive(true);
                nameInputField.text = "";
            }
            else { nameInputField.text = value; errorText.gameObject.SetActive(false); }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerName()
    {
        playerName = nameInputField.text;
    }

    public void StartGame()
    {
        if (playerName != null)
        {
            SceneManager.LoadScene(1);
        }

    }
}