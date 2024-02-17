using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Radius_Fire_Light : MonoBehaviour
{

    private UnityEngine.Rendering.Universal.Light2D myLight;
    float minRazm_Radius_Light = 0.5f;
    float maxRazm_Radius_Light = 0;
    float Changer = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        myLight.falloffIntensity = minRazm_Radius_Light;
    }

    // Update is called once per frame
    void Update()
    {
        if(myLight.falloffIntensity >= 0.5)
        {
            Changer = -Changer;
        }
        else if(myLight.falloffIntensity <= 0)
        {
            Changer = -Changer;
        }
        myLight.falloffIntensity += Changer*4 * Time.deltaTime;
        myLight.intensity += Changer*4 * Time.deltaTime;
    }

}
