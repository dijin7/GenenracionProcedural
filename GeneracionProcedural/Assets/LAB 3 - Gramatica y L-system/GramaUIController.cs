using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GramaUIController : MonoBehaviour
{
    public TMP_InputField inputIteraciones;
    public TMP_InputField inputAngulo;
    public TMP_InputField inputLargo;
    public TMP_InputField inputCantidadPlantas;
    public Button botonGenerar;
    public Button botonAleatorizarRegla;
    public GameObject plantaPrefab; // Asigna el prefab de la planta en el Inspector

    private string reglaAleatoriaActual = "FF+[+F-F-F]-[-F+F+F]"; // Valor por defecto

    void Start()
    {
        botonGenerar.onClick.AddListener(GenerarArboles);
        botonAleatorizarRegla.onClick.AddListener(AleatorizarRegla);
        // Si quieres valores por defecto, asígnalos manualmente aquí
        inputIteraciones.text = "2";
        inputAngulo.text = "25";
        inputLargo.text = "0.2";
        inputCantidadPlantas.text = "1";
    }

    void GenerarArboles()
    {
        // Elimina todas las líneas de árboles anteriores
        foreach (var linea in GameObject.FindGameObjectsWithTag("Line"))
            Destroy(linea);

        // Elimina las plantas actuales
        foreach (var planta in GameObject.FindGameObjectsWithTag("PlantaLSystem"))
            Destroy(planta);

        // Elimina cualquier sembrador anterior
        var sembradores = FindObjectsOfType<Grama>();
        foreach (var s in sembradores)
            Destroy(s.gameObject);

        // Crea un nuevo sembrador
        GameObject nuevoSembrador = new GameObject("SembradorGrama");
        Grama gramaSembrador = nuevoSembrador.AddComponent<Grama>();
        gramaSembrador.plantaPrefab = plantaPrefab;
        gramaSembrador.iterations = int.Parse(inputIteraciones.text);
        gramaSembrador.angle = float.Parse(inputAngulo.text);
        gramaSembrador.length = float.Parse(inputLargo.text);
        gramaSembrador.cantidadPlantas = int.Parse(inputCantidadPlantas.text);
        gramaSembrador.axiom = "F";
        gramaSembrador.reglaAleatoriaActual = reglaAleatoriaActual;
    }

    void AleatorizarRegla()
    {
        // Crea un objeto temporal para usar el generador
        Grama temp = new GameObject("TempGrama").AddComponent<Grama>();
        reglaAleatoriaActual = temp.GenerarReglaAleatoria();
        Destroy(temp.gameObject);
        Debug.Log("Regla aleatoria generada: " + reglaAleatoriaActual);
    }
}