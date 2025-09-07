using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelTerrenoPerlin : MonoBehaviour
{
    
    public PerlinNoise perlinNoise; ///por razones que no comprendo "generador" tiene como nombre en codigo "Script"

    public Button generarCieloBoton;

    public TMP_InputField cantidadCirculo; 
    public TMP_InputField radioMinAgua;
    public TMP_InputField radioMaxAgua;

    private int circuloNumero;
    private float radioMinNumero;
    private float radioMaxNumero;

    void Start()
    {
        // Inicializar los valores con los del PerlinNoise
        circuloNumero = perlinNoise.cantidadCirculosAgua;
        radioMinNumero = perlinNoise.radioMinAgua;
        radioMaxNumero = perlinNoise.radioMaxAgua;

        cantidadCirculo.text = circuloNumero.ToString();
        radioMinAgua.text = radioMinNumero.ToString();
        radioMaxAgua.text = radioMaxNumero.ToString();

        generarCieloBoton.onClick.AddListener(presionarGenerador);
        cantidadCirculo.onEndEdit.AddListener(text => validarNumero(text, value => circuloNumero = value));
        radioMinAgua.onEndEdit.AddListener(text => validarNumero(text, value => radioMinNumero = value));
        radioMaxAgua.onEndEdit.AddListener(text => validarNumero(text, value => radioMaxNumero = value));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void presionarGenerador()
    {
        // Actualizar los parámetros y la textura
        perlinNoise.ActualizarCharcosAgua(circuloNumero, radioMinNumero,radioMaxNumero);
    }

    void validarNumero(string textoEntregado, System.Action<int> numeroParaActualizar)
    {
        int numero;

         if (int.TryParse(textoEntregado, out numero))
        {
            numeroParaActualizar(numero);
            Debug.Log("Numero valido");
        }
        else
        {
            Debug.LogWarning("No ingreso entero");
        }
    }

    private Color ColorDesdeNombre(string nombre)
    {
        switch (nombre)
        {
            case "Azul": return Color.blue;
            case "Blanco": return Color.white;
            case "Verde": return Color.green;
            case "Negro": return Color.black;
            case "Amarillo": return Color.yellow;
            default: return Color.white;
        }
    }
}
