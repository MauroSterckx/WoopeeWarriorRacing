using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
//    public InputField inputField;

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

    public void SetName()
    {
        //string inputText = inputText
        //textMeshProText.text = inputText;
    }
}
