using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text gameStateText;
    
    private void Start()
    {
        GameController.Instance.OnGameStateChanged += ChangeGameStateText;
        ChangeGameStateText(false);
    }

    private void ChangeGameStateText(bool gameOn)
    {
        gameStateText.text = gameOn ? $"<color=#009F00>Game On</color>" : $"<color=#9F0000>Game Off</color>";
    }
}
