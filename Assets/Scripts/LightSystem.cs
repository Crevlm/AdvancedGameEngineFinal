using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] private float lightLevel = 1f; //how much light you've collected
    [SerializeField] private float lightIncreaseAmount = 0.5f; // how much each shard increases the brightness for the world
    [SerializeField] private Light worldLight; //the main light
    [SerializeField] private float worldLightMult = 0.2f; // how strong the light becomes with each level
    
    public float CurrentLightLevel => lightLevel; // Public getter for current light level
    public System.Action OnLightChanged; // Event triggered when light level changes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize the basic light
        if (worldLight != null)
        {
            worldLight.intensity = lightLevel * worldLightMult; //set initial light intensity
        }

        RenderSettings.ambientIntensity = lightLevel * 0.1f; //brighten the ambient light
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLight()
    {
        lightLevel += lightIncreaseAmount;

        if (worldLight != null)
        {
            worldLight.intensity = lightLevel * worldLightMult;
        }

        RenderSettings.ambientIntensity = lightLevel * 0.1f;

        Debug.Log("Light Shard Collected! World brightness now: " + lightLevel);
    
        OnLightChanged?.Invoke();
    }
}
