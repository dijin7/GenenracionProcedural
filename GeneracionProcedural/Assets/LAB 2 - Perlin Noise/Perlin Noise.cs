using UnityEngine;
using UnityEngine.Rendering;

public class NewMonoBehaviourScript : MonoBehaviour
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

    public float zoom;
    public float xCordZoom;
    public float yCordZoom;
    private float contadorMovimientoFondo;



    void Start()
    {
        zoom = 20f;
        contadorMovimientoFondo = 0f;


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
        if(contadorMovimientoFondo > 5)
        {
            xCordZoom++;
            render.material.mainTexture = GenerarTextura();
            contadorMovimientoFondo = 0f;
        }


    }



    
    Texture2D GenerarTextura()
    {
        //Se crea la textura del tamaño en particular
        Texture2D textura = new Texture2D(width, height);

        //se agarra cada pixel y se calcula su color y se establece su color en base a calcularColor()
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
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
        float sample = Mathf.PerlinNoise(xCordenada, yCordenada);

        return Color.Lerp(Color.blue, Color.white, sample); //Color.lerp mezcla los colores azul y blanco segun en  base al sample
    }




}
