using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float requiredLightShards = 3.5f;
    public LightSystem lightSystem;
    public Collider doorCollider;

    private bool isOpen = false;

    /// <summary>
    /// Subscribes to the light system's light change event to monitor and handle unlock conditions.
    /// </summary>
    /// <remarks>This method attaches the <see cref="CheckUnlock"/> method to the <c>OnLightChanged</c> event
    /// of the light system, if the light system is not null. Ensure that the <c>lightSystem</c> is properly initialized
    /// before calling this method.</remarks>
    void Start()
    {
        if (lightSystem != null)
            lightSystem.OnLightChanged += CheckUnlock;
    }

/// <summary>
/// Checks whether the door can be unlocked based on the current light level and unlocks it if the conditions are met.
/// </summary>
/// <remarks>The door will be unlocked if it is currently closed and the current light level is greater than or
/// equal to the required light shards.</remarks>
    void CheckUnlock()
    {
        if (!isOpen && lightSystem.CurrentLightLevel >= requiredLightShards)
        {
            UnlockDoor();
        }
    }

    /// <summary>
    /// Unlocks the door, allowing it to be opened.
    /// </summary>
    /// <remarks>This method sets the door to an open state and disables the door's collider,  It also logs a message indicating that the door has been unlocked.</remarks>
    void UnlockDoor()
    {
        isOpen = true;

        if (doorCollider != null)
            doorCollider.enabled = false;

        Debug.Log("Door Unlocked!");
    }
}