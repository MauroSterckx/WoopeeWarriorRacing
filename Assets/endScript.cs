using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI outputText;
    void Start()
    {

        outputText.text = $"{GameData.Placement} place";

    }
}
