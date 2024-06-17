using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public List<Material> materials = new List<Material>(); //blue, red, yellow

    public GameObject Car;

    // Start is called before the first frame update
    void Start()
    {
        string color = GameData.CarColor;

        Renderer renderer = Car.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (color.Equals("BLUE"))
                renderer.material = materials[0];
            if (color.Equals("RED"))
                renderer.material = materials[1];
            if (color.Equals("YELLOW"))
                renderer.material = materials[2];
        }
    }


    private IEnumerator Pause(int p)
    {
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }
}

