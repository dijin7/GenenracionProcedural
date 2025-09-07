using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PerlinNoise : MonoBehaviour
{
    /// Ignacio - script basado en el siguiente video de youtube
    /// youtube: https://www.youtube.com/watch?v=bG0uEXV6aHQ

    // 80 x 64 es la escuala de quad
    // probare con x5 (400 x 320)


    ///division teorica del material que me gustaria probar
    public int width = 400;
    public int height = 320;

    public Renderer render;
    public Material material;

    [SerializeField] public float zoom;
    public Color colorInicial = Color.blue;
    public Color colorFinal = Color.white;

    [SerializeField] private float xCordZoom;
    [SerializeField] private float yCordZoom;
    [SerializeField] private float contadorMovimientoFondo;

    private int nUnique = 256;
    private Vector2[] cells2d;
    private int[] perm;

    public GameObject bloqueTierraPrefab; // Asigna el prefab caf� en el inspector
    public GameObject bloqueAguaPrefab;   // Asigna el prefab celeste en el inspector

    public int cantidadCirculosAgua = 5;
    public float radioMinAgua = 5f;
    public float radioMaxAgua = 12f;

    void Start()
    {
        zoom = 20f;
        contadorMovimientoFondo = 0f;

        InitPerlin();

        render = GetComponent<Renderer>();
        if (render != null)
            render.material.mainTexture = GenerarTextura();

        // Solo genera terreno si est� en la escena espec�fica
        if (SceneManager.GetActiveScene().name == "Segunda forma")
        {
            GenerarTerrenoConPerlin();
        }
    }

    void FixedUpdate()
    {
        contadorMovimientoFondo++;
        if (contadorMovimientoFondo > 5)
        {
            xCordZoom++;
            if (render != null)
                render.material.mainTexture = GenerarTextura();
            contadorMovimientoFondo = 0f;
        }
    }

    public void ActualizarParametrosPerlin(int nuevoWidth, int nuevoHeight, float nuevoZoom, float nuevoXCordZoom, float nuevoYCordZoom)
    {
        width = nuevoWidth;
        height = nuevoHeight;
        zoom = nuevoZoom;
        xCordZoom = nuevoXCordZoom;
        yCordZoom = nuevoYCordZoom;

        // Regenerar la textura con los nuevos par�metros
        if (render != null)
            render.material.mainTexture = GenerarTextura();
    }

    Texture2D GenerarTextura()
    {
        //Se crea la textura del tama�o en particular
        Texture2D textura = new Texture2D(width, height);

        //se agarra cada pixel y se calcula su color y se establece su color en base a calcularColor()
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color colorNuevo = calcularColor(x, y);
                textura.SetPixel(x, y, colorNuevo);
            }

        }



        //aplicar la textura
        textura.Apply();


        return textura;
    }

    Color calcularColor(int x, int y)
    {
        //seg�n entiendo esto se hace para que las diferencias entre pixel a y b no sean tan fuertemente distintos, y que sean mas una graduaci�n, pero no lo entiendo del todo
        float xCordenada = (float)x / width * zoom + xCordZoom;
        float yCordenada = (float)y / height * zoom + yCordZoom;

        //quiero preguntarle al profe con respecto a la matematica de perlinNoise 
        float sample = Noise2D(xCordenada, yCordenada);

        return Color.Lerp(colorInicial, colorFinal, sample); //Color.lerp mezcla los colores azul y blanco segun en  base al sample
    }

    private void InitPerlin()
    {
        cells2d = new Vector2[nUnique];
        perm = new int[nUnique * 2];
        for (int i = 0; i < nUnique; i++)
        {
            float angle = Random.value * Mathf.PI * 2;
            cells2d[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            perm[i] = i;
        }
        for (int i = 0; i < nUnique; i++)
        {
            int j = Random.Range(0, nUnique);
            int tmp = perm[i];
            perm[i] = perm[j];
            perm[j] = tmp;
            perm[i + nUnique] = perm[i];
        }
    }

    private float Dot(Vector2 g, Vector2 d)
    {
        return g.x * d.x + g.y * d.y;
    }

    private float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

    // Funci�n de suavizado
    private float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }
    private float Noise2D(float x, float y)
    {
        int _x = Mathf.FloorToInt(x);
        int _y = Mathf.FloorToInt(y);
        x = x - _x;
        y = y - _y;

        int _x0 = _x % nUnique;
        int _y0 = _y % nUnique;
        int _x1 = (_x + 1) % nUnique;
        int _y1 = (_y + 1) % nUnique;

        Vector2 g00 = cells2d[(_x0 + perm[_y0]) % nUnique];
        Vector2 g10 = cells2d[(_x1 + perm[_y0]) % nUnique];
        Vector2 g01 = cells2d[(_x0 + perm[_y1]) % nUnique];
        Vector2 g11 = cells2d[(_x1 + perm[_y1]) % nUnique];

        Vector2 d00 = new Vector2(x, y);
        Vector2 d10 = new Vector2(x - 1, y);
        Vector2 d01 = new Vector2(x, y - 1);
        Vector2 d11 = new Vector2(x - 1, y - 1);

        float in00 = Dot(g00, d00);
        float in10 = Dot(g10, d10);
        float in01 = Dot(g01, d01);
        float in11 = Dot(g11, d11);

        float l1 = Lerp(in00, in10, Fade(x));
        float l2 = Lerp(in01, in11, Fade(x));
        return Lerp(l1, l2, Fade(y)) + 0.5f;
    }


    public void ChangeZoom(int value)
    {
        zoom = value;
    }

    void GenerarTerrenoConPerlin()
    {
        // 1. Genera los c�rculos de agua
        Vector2[] centrosAgua = new Vector2[cantidadCirculosAgua];
        float[] radiosAgua = new float[cantidadCirculosAgua];
        for (int i = 0; i < cantidadCirculosAgua; i++)
        {
            float centroX = Random.Range(width * 0.2f, width * 0.8f);
            float centroY = Random.Range(height * 0.2f, height * 0.8f);
            centrosAgua[i] = new Vector2(centroX, centroY);
            radiosAgua[i] = Random.Range(radioMinAgua, radioMaxAgua);
        }

        // 2. Genera el terreno y decide si cada bloque es agua o tierra
        float offsetX = (width * 0.1f) / 2f;
        float offsetY = (height * 0.5f * 0.1f) / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height * 0.5f; y++)
            {
                Vector3 posicion = new Vector3(x * 0.1f - offsetX, y * 0.1f - offsetY, 0);

                bool esAgua = false;
                Vector2 punto = new Vector2(x, y);

                for (int i = 0; i < cantidadCirculosAgua; i++)
                {
                    if (Vector2.Distance(punto, centrosAgua[i]) < radiosAgua[i])
                    {
                        esAgua = true;
                        break;
                    }
                }

                if (esAgua)
                    Instantiate(bloqueAguaPrefab, posicion, Quaternion.identity);
                else
                    Instantiate(bloqueTierraPrefab, posicion, Quaternion.identity);
            }
        }
    }
}
