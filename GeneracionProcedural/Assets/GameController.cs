using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private string[] escenas = { "PrimeraEscena", "SegundaEscena" };
    private int escenaActual = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Conserva el objeto entre escenas
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            escenaActual = (escenaActual + 1) % escenas.Length;
            SceneManager.LoadScene(escenas[escenaActual]);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            escenaActual = (escenaActual - 1 + escenas.Length) % escenas.Length;
            SceneManager.LoadScene(escenas[escenaActual]);
        }
    }
}

