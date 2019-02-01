using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;

    [SerializeField] private LobbyCanvas _lobbyCanvas;

    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    [SerializeField] private CurrentRoomCanvas _currentRoomCanvas;

    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }

    [SerializeField] private MainMenuCanvas _mainMenuCanvas;

    public MainMenuCanvas MainMenuCanvas
    {
        get { return _mainMenuCanvas; }
    }

    [SerializeField] private OptionsMenuCanvas _optionsMenuCanvas;

    public OptionsMenuCanvas OptionsMenuCanvas => _optionsMenuCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}