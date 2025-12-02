using UnityEngine;

public class PauseControlManager : MonoBehaviour
{
    public static PauseControlManager Instance;

    [Header("Player Control Scripts")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour cameraController;

    private void Awake()
    {
        Instance = this;
    }

    public void DisableControl()
    {
        if (playerMovement) playerMovement.enabled = false;
        if (cameraController) cameraController.enabled = false;
    }

    public void EnableControl()
    {
        if (playerMovement) playerMovement.enabled = true;
        if (cameraController) cameraController.enabled = true;
    }
}
