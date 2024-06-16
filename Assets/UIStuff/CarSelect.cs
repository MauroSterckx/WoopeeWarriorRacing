using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelect : MonoBehaviour
{
    public GameObject[] prefabsToToggle;
    private int activePrefabIndex = 0;

    // Voeg de prefabs toe in de Inspector of hardcode ze hier
    public GameObject F1RED;
    public GameObject F1YELLOW;
    public GameObject F1BLUE;

    // username
    public string USERNAME;
    public TMP_Text textMeshProText;
    public TMP_Text inputText;

    public TMP_InputField inputField;
    public TextMeshProUGUI outputText;

    // Map
    public Toggle toggle1;
    public Toggle toggle2;

    // Global stuff
    void Start()
    {
        // Als je de prefabs in het script hardcodeert, kun je ze hier toewijzen
        prefabsToToggle = new GameObject[] { F1RED, F1YELLOW, F1BLUE };
    }

    public void NextPrefab()
    {
        // Zet de huidige actieve prefab op non-active
        prefabsToToggle[activePrefabIndex].SetActive(false);

        // Verhoog de index van de actieve prefab
        activePrefabIndex = (activePrefabIndex + 1) % prefabsToToggle.Length;

        // Zet de volgende prefab op active
        prefabsToToggle[activePrefabIndex].SetActive(true);
    }

    public void PreviousPrefab()
    {
        // Zet de huidige actieve prefab op non-active
        prefabsToToggle[activePrefabIndex].SetActive(false);

        // Verminder de index van de actieve prefab
        activePrefabIndex = (activePrefabIndex - 1 + prefabsToToggle.Length) % prefabsToToggle.Length;

        // Zet de vorige prefab op active
        prefabsToToggle[activePrefabIndex].SetActive(true);
    }

    // Methode om de ingevoerde tekst uit te lezen
    public void ReadInputFieldValue()
    {
        string inputText = inputField.text;
        Debug.Log("Ingevoerde tekst: " + inputText);

        // Pas de tekst van de uitvoer TextMeshPro Text UI-component aan
        outputText.text = inputText;
    }
    public void ToggleChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            // Schakel de andere toggle uit
            if (changedToggle == toggle1)
            {
                toggle2.isOn = false;
                Debug.Log("toggle 1 is on");
            }
            else
            {
                toggle1.isOn = false;
                Debug.Log("toggle 2 is on");
            }

            // Werk de output text bij met de ingevoerde tekst

        }
    }

    public void Ready()
    {
        // User klikt op ready om game te starten

        // Car
        if (activePrefabIndex == 0)
        {
            GameData.CarColor = "RED";
        }
        if (activePrefabIndex == 1)
        {
            GameData.CarColor = "YELLOW";
        }
        else
        {
            GameData.CarColor = "BLUE";
        }

        // Username
        GameData.Username = outputText.text;

        // Map
        if (toggle1.isOn)
        {
            // map 1
            Debug.Log("gerbenScene");
            SceneManager.LoadScene("gerbenScene");
        }
        else
        {
            // map 2
            SceneManager.LoadScene("vierkantMap");
        }
    }
}
