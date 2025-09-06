using UnityEngine;
using UnityEngine.Rendering;

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
    [SerializeField] private float xCordZoom;
    [SerializeField] private float yCordZoom;
    [SerializeField] private float contadorMovimientoFondo;

    private int nUnique = 256;
    private Vector2[] cells2d;
    private int[] perm;



    void Start()
    {
        zoom = 20f;
        contadorMovimientoFondo = 0f;

        InitPerlin();


        //agarro el material
        render = GetComponent<Renderer>();
        render.material.mainTexture = GenerarTextura();


        //modificación de color (sin motivo aparente)
        //material.color = Color.blue;
    }

    void FixedUpdate()
    {

        //Cosas para que el fondo se mueva 
        contadorMovimientoFondo++;
        if (contadorMovimientoFondo > 5)
        {
            xCordZoom++;
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

        // Regenerar la textura con los nuevos parámetros
        render.material.mainTexture = GenerarTextura();
    }

    Texture2D GenerarTextura()
    {
        //Se crea la textura del tamaño en particular
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
        //según entiendo esto se hace para que las diferencias entre pixel a y b no sean tan fuertemente distintos, y que sean mas una graduación, pero no lo entiendo del todo
        float xCordenada = (float)x / width * zoom + xCordZoom;
        float yCordenada = (float)y / height * zoom + yCordZoom;

        //quiero preguntarle al profe con respecto a la matematica de perlinNoise 
        float sample = Noise2D(xCordenada, yCordenada);

        return Color.Lerp(Color.blue, Color.white, sample); //Color.lerp mezcla los colores azul y blanco segun en  base al sample
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

    // Función de suavizado
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
}
