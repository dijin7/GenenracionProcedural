using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{

    public SCRIPT generador; ///por razones que no comprendo "generador" tiene como nombre en codigo "Script"

    public Button generarTerrenoBoton;


    void Start()
    {
        generarTerrenoBoton.onClick.AddListener(presionarGenerador);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void presionarGenerador()
    {
        generador.EliminarTerreno();
        generador.Generar();
    }

}
