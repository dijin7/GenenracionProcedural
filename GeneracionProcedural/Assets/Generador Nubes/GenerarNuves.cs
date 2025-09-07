using UnityEngine;

public class GenerarNuves : MonoBehaviour
{
    [Header("Prefab de círculo para la nube")]
    public GameObject nubeCirculoPrefab;

    [Header("Rango de generación en X")]
    public float minX = -8f;
    public float maxX = 8f;

    [Header("Altura de generación")]
    public float spawnY = 6f;

    [Header("Intervalo entre nubes (segundos)")]
    public float intervalo = 2f;

    [Header("Velocidad de caída de la nube")]
    public float velocidadNube = 1f;

    [Header("Cantidad mínima y máxima de círculos por nube")]
    public int minCirculos = 5;
    public int maxCirculos = 12;

    [Header("Ancho máximo de la nube")]
    public float anchoNube = 3f;

    [Header("Altura máxima de la nube")]
    public float alturaNube = 1.5f;

    private float tiempoUltimaNube = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - tiempoUltimaNube > intervalo)
        {
            GenerarNube();
            tiempoUltimaNube = Time.time;
        }
    }

    void GenerarNube()
    {
        float x = Random.Range(minX, maxX);
        Vector3 posicion = new Vector3(x, spawnY, 0);
        GameObject nube = new GameObject("NubeProcedural");

        int anchoNubeInt = Mathf.RoundToInt(anchoNube);
        int alturaBase = Mathf.RoundToInt(alturaNube);

        int rangoAlturaMinima = 1;
        int rangoAlturaMaxima = alturaBase;

        int nCirculosMinimoEntreCentroYBorde = 1;
        int nCirculosMaximoEntreCentroYBorde = alturaBase;

        int altura = alturaBase;

        for (int i = 0; i < anchoNubeInt; i++)
        {
            int alturaMinima = altura - rangoAlturaMinima;
            int alturaMaxima = altura + rangoAlturaMaxima;

            altura = Random.Range(alturaMinima, alturaMaxima);

            int MinimaDistanciaAparicionBorde = altura - nCirculosMinimoEntreCentroYBorde;
            int MaximaDistanciaAparicionBorde = altura - nCirculosMaximoEntreCentroYBorde;

            int TotalDistanciaAparicionBorde = Random.Range(MinimaDistanciaAparicionBorde, MaximaDistanciaAparicionBorde);

            for (int j = 0; j < altura; j++)
            {
                float posX = posicion.x + (i - anchoNubeInt / 2f) * 0.5f;
                float posY = posicion.y + (j - alturaBase / 2f) * 0.5f;

                // Instancia la sombra primero
                GameObject sombra = Instantiate(nubeCirculoPrefab, new Vector3(posX - 0.15f, posY - 0.1f, 0), Quaternion.identity, nube.transform);
                float escalaSombra = Random.Range(0.75f, 1.25f);
                sombra.transform.localScale = new Vector3(escalaSombra, escalaSombra, 1);

                // Cambia el color y la opacidad de la sombra
                var srSombra = sombra.GetComponent<SpriteRenderer>();
                if (srSombra != null)
                    srSombra.color = new Color(0, 0, 0, 0.25f); // negro con opacidad baja

                // Instancia el círculo principal
                GameObject circulo = Instantiate(nubeCirculoPrefab, new Vector3(posX, posY, 0), Quaternion.identity, nube.transform);
                float escala = Random.Range(0.7f, 1.2f);
                circulo.transform.localScale = new Vector3(escala, escala, 1);
            }
        }

        nube.AddComponent<MovimientoNube>().velocidad = velocidadNube;
    }
}

// Script para mover la nube hacia abajo y destruirla al salir de la pantalla
public class MovimientoNube : MonoBehaviour
{
    public float velocidad = 1f;
    //cambios
    void Update()
    {
        transform.position += Vector3.down * velocidad * Time.deltaTime;

        // Si la nube sale de la pantalla, destrúyela
        if (transform.position.y < -12f)
        {
            Destroy(gameObject);
        }
    }
}
