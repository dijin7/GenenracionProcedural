using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelCielo : MonoBehaviour
{
    
    public PerlinNoise perlinNoise; ///por razones que no comprendo "generador" tiene como nombre en codigo "Script"

    public Button generarCieloBoton;

    public TMP_InputField alturaInput; 
    public TMP_InputField anchoInput;
    public TMP_InputField zoomInput;

    private int alturaNumero;
    private int anchoNumero;
    private float zoomNumero;

    public TMP_Dropdown ColorInicial;
    public string ColorInicialValor;

    public TMP_Dropdown ColorFinal;
    public string ColorFinalValor;



    void Start()
    {
        // Inicializar los valores con los del PerlinNoise
        alturaNumero = perlinNoise.height;
        anchoNumero = perlinNoise.width;
        zoomNumero = perlinNoise.zoom;

        alturaInput.text = alturaNumero.ToString();
        anchoInput.text = anchoNumero.ToString();
        zoomInput.text = zoomNumero.ToString();

        generarCieloBoton.onClick.AddListener(presionarGenerador);
        alturaInput.onEndEdit.AddListener(text => validarNumero(text, value => alturaNumero = value));
        anchoInput.onEndEdit.AddListener(text => validarNumero(text, value => anchoNumero = value));
        zoomInput.onEndEdit.AddListener(text => validarNumero(text, value => zoomNumero = value));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void presionarGenerador()
    {
        // Obtener el nombre del color seleccionado en cada dropdown
        string colorInicialNombre = ColorInicial.options[ColorInicial.value].text;
        string colorFinalNombre = ColorFinal.options[ColorFinal.value].text;

        // Convertir el nombre a Color
        Color colorInicial = ColorDesdeNombre(colorInicialNombre);
        Color colorFinal = ColorDesdeNombre(colorFinalNombre);

        // Asignar los colores al PerlinNoise
        perlinNoise.colorInicial = colorInicial;
        perlinNoise.colorFinal = colorFinal;

        // Actualizar los parámetros y la textura
        perlinNoise.ActualizarParametrosPerlin(anchoNumero, alturaNumero, zoomNumero, 0f, 0f);
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
