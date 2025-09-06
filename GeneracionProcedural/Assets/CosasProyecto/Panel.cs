using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class Panel : MonoBehaviour
{
    
    public SCRIPT generador; ///por razones que no comprendo "generador" tiene como nombre en codigo "Script"

    public UnityEngine.UI.Button generarTerrenoBoton;

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

    public TMP_Dropdown DropSuperior;
    public string DropSuperiorValor;

    public TMP_Dropdown DropCapaMedia;
    public string DropCapaMediaValor;

    public TMP_Dropdown DropCapaInferior;
    public string DropCapaInferiorValor;


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






        ///inicialización de los drop
        if (DropSuperior != null)
        {
            DropSuperiorValor = DropSuperior.options[DropSuperior.value].text;
            DropSuperior.onValueChanged.AddListener(indice => DetectarNumeroDropdown(DropSuperior, indice, valor => DropSuperiorValor = valor));
        }

        if (DropCapaMedia != null)
        {
            DropCapaMediaValor = DropCapaMedia.options[DropCapaMedia.value].text;
            DropCapaMedia.onValueChanged.AddListener(indice => DetectarNumeroDropdown(DropCapaMedia, indice, valor => DropCapaMediaValor = valor));
        }
        if (DropCapaInferior != null)
        {
            DropCapaInferiorValor = DropCapaInferior.options[DropCapaInferior.value].text;
            DropCapaInferior.onValueChanged.AddListener(indice => DetectarNumeroDropdown(DropCapaInferior, indice, valor => DropCapaInferiorValor = valor));
        }
            
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


    void DetectarNumeroDropdown(TMP_Dropdown drop, int numero, System.Action<string> asignacionNumeroDrop)
    {
        string opcionElegida = drop.options[numero].text;
        asignacionNumeroDrop(opcionElegida);
        Debug.Log(asignacionNumeroDrop);
        
        //esto es flojera, luego debo ver si lo mejoro:
        generador.nombrePasto = DropSuperiorValor;
        generador.nombreTierra = DropCapaMediaValor;
        generador.nombrePiedra = DropCapaInferiorValor;

        generador.pasto = Resources.Load<GameObject>(DropSuperiorValor);
        generador.tierra = Resources.Load<GameObject>(DropCapaMediaValor);
        generador.piedra = Resources.Load<GameObject>(DropCapaInferiorValor);
    }

}
