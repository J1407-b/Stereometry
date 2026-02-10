using UnityEngine;

public class OpenCloseDotSettings : MonoBehaviour
{
    [SerializeField] private GameObject DotSettings;
    public void OpenDotSettings()
    {
        DotSettings.SetActive(true);
    }
    public void CloseDotSettings()
    {
        DotSettings.SetActive(false);
    }
}
