using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public event Action<bool> OnGameStateChanged;

    [SerializeField] private Grid grid;
    [SerializeField] private int lifecycleDelay;
    [SerializeField] private bool isParallel;

    private Camera _cam;
    private bool _gameStarted;
    private Thread _gameCycleThread;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _cam = Camera.main;
        _gameStarted = false;

        _gameCycleThread = new Thread(new ThreadStart(GameCycle));
    }

    private void Update()
    {
        if (_gameStarted)
        {
            Thread.Sleep(lifecycleDelay);
            grid.GameCycle();
            return;
        }

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Input.GetMouseButtonDown(0)) { return; }
        if (!Physics.Raycast(ray, out hit)) { return; }
        if (!hit.collider.TryGetComponent(out Cell cell)) { return; }

        cell.ChangeState();
    }

    public void GameStartedTrigger()
    {
        _gameStarted = !_gameStarted;
        OnGameStateChanged?.Invoke(_gameStarted);

        //if (_gameStarted) { _gameCycleThread.Start(); }
        // else { _gameCycleThread.Suspend(); }
    }

    private void GameCycle()
    {
        if (_gameStarted)
        {
            var sw = Stopwatch.StartNew();

            Thread.Sleep(lifecycleDelay);
            grid.GameCycle();

            sw.Stop();

            UnityEngine.Debug.Log($"Delay: {sw.ElapsedMilliseconds}");
        }
    }
}
