using UnityEngine;

public class LevelStartManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructionsPanel.SetActive(true);
        Time.timeScale = 0f;

        player.canLook = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

  public void BeginGame()
    {
        instructionsPanel.SetActive(false);
        Time.timeScale = 1f;

        player.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
