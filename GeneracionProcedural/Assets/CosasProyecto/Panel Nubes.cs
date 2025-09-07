using TMPro;
using UnityEngine;

public class PanelNubes : MonoBehaviour
{

    public GenerarNuves generador; ///por razones que no comprendo "generador" tiene como nombre en codigo "Script"

    public UnityEngine.UI.Button generarTerrenoBoton;

    public TMP_InputField minX;
    public TMP_InputField maxX;
    public TMP_InputField spawnY;
    public TMP_InputField intervalo;
    public TMP_InputField velocidadNube;
    public TMP_InputField minCirculos;
    public TMP_InputField maxCirculos;
    public TMP_InputField anchoNube;
    public TMP_InputField alturaNube;

    private float minXNumero;
    private float maxYNumero;
    private float spawnYNumero;
    private float intervaloNumero;
    private float velocidadNubeNumero;
    private int minCirculosNumero;
    private int maxCirculosNumero;
    private float anchoNubeNumero;
    private float alturaNubeNumero;

    void Start()
    {
        generarTerrenoBoton.onClick.AddListener(presionarGenerador);

        minX.onEndEdit.AddListener(text => validarNumero(text, value => minXNumero = value));
        maxX.onEndEdit.AddListener(text => validarNumero(text, value => maxYNumero = value));
        spawnY.onEndEdit.AddListener(text => validarNumero(text, value => spawnYNumero = value));
        intervalo.onEndEdit.AddListener(text => validarNumero(text, value => intervaloNumero = value));
        velocidadNube.onEndEdit.AddListener(text => validarNumero(text, value => velocidadNubeNumero = value));
        minCirculos.onEndEdit.AddListener(text => validarNumero(text, value => minCirculosNumero = value));
        maxCirculos.onEndEdit.AddListener(text => validarNumero(text, value => maxCirculosNumero = value));
        anchoNube.onEndEdit.AddListener(text => validarNumero(text, value => anchoNubeNumero = value));
        alturaNube.onEndEdit.AddListener(text => validarNumero(text, value => alturaNubeNumero = value));

        minXNumero = generador.minX;
        maxYNumero = generador.maxX;
        spawnYNumero = generador.spawnY;
        intervaloNumero = generador.intervalo;
        velocidadNubeNumero = generador.velocidadNube;
        minCirculosNumero = generador.minCirculos;
        maxCirculosNumero = generador.maxCirculos;
        anchoNubeNumero = generador.anchoNube;
        alturaNubeNumero = generador.alturaNube;


        minX.text = minXNumero.ToString();
        maxX.text = maxYNumero.ToString();
        spawnY.text = spawnYNumero.ToString();
        intervalo.text = intervaloNumero.ToString();
        velocidadNube.text = velocidadNubeNumero.ToString();
        minCirculos.text = minCirculosNumero.ToString();
        maxCirculos.text = maxCirculosNumero.ToString();
        anchoNube.text = anchoNubeNumero.ToString();
        alturaNube.text = alturaNubeNumero.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void presionarGenerador()
    {
        //generador.EliminarTerreno();

        generador.minX = minXNumero;
        generador.maxX = maxYNumero;
        generador.spawnY = spawnYNumero;
        generador.intervalo = intervaloNumero;
        generador.velocidadNube = velocidadNubeNumero;
        generador.minCirculos = minCirculosNumero;
        generador.maxCirculos = maxCirculosNumero;
        generador.anchoNube = anchoNubeNumero;
        generador.alturaNube = alturaNubeNumero;

        //generador.Generar();
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
