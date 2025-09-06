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

}
