using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel : MonoBehaviour
{
    
    public SCRIPT generador; ///por razones que no comprendo "generador" tiene como nombre en codigo "Script"

    public Button generarTerrenoBoton;

    public TMP_InputField alturaInput; 
    public TMP_InputField anchoInput;
    public TMP_InputField nBloquesMinInput;
    public TMP_InputField nBloquesMaxInput;
    public TMP_InputField nRangoMinInput;
    public TMP_InputField nRangoMaxInput;

    private int alturaNumero;
    private int anchoNumero;
    private int nBloquesMinimoNumero;
    private int nBloquesMaximoNumero;
    private int rangoAlturaMinima;
    private int rangoAlturaMaxima;



    void Start()
    {
        generarTerrenoBoton.onClick.AddListener(presionarGenerador);
        alturaInput.onEndEdit.AddListener(text => validarNumero(text, value => alturaNumero = value));
        anchoInput.onEndEdit.AddListener(text => validarNumero(text, value => anchoNumero = value));
        nBloquesMinInput.onEndEdit.AddListener(text => validarNumero(text, value => nBloquesMinimoNumero = value));
        nBloquesMaxInput.onEndEdit.AddListener(text => validarNumero(text, value => nBloquesMaximoNumero = value));
        nRangoMinInput.onEndEdit.AddListener(text => validarNumero(text, value => rangoAlturaMinima = value));
        nRangoMaxInput.onEndEdit.AddListener(text => validarNumero(text, value => rangoAlturaMaxima = value));

        alturaNumero = generador.altura;
        anchoNumero = generador.ancho;
        nBloquesMaximoNumero = generador.nBloquesMaximoEntrePiedraYPasto;
        nBloquesMinimoNumero = generador.nBloquesMinimoEntrePiedraYPasto;
        rangoAlturaMaxima = generador.rangoAlturaMaxima;
        rangoAlturaMinima = generador.rangoAlturaMinima;

        alturaInput.text = alturaNumero.ToString();
        anchoInput.text = anchoNumero.ToString();
        nBloquesMinInput.text = nBloquesMinimoNumero.ToString();
        nBloquesMaxInput.text = nBloquesMaximoNumero.ToString();
        nRangoMinInput.text = rangoAlturaMinima.ToString();
        nRangoMaxInput.text = rangoAlturaMaxima.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void presionarGenerador()
    {
        generador.EliminarTerreno();
        
        generador.altura = alturaNumero;
        generador.ancho = anchoNumero;
        generador.nBloquesMaximoEntrePiedraYPasto = nBloquesMaximoNumero;
        generador.nBloquesMinimoEntrePiedraYPasto = nBloquesMinimoNumero;
        generador.rangoAlturaMaxima = rangoAlturaMaxima;
        generador.rangoAlturaMinima = rangoAlturaMinima;

        generador.Generar();
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
