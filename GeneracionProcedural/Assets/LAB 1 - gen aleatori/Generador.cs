using UnityEngine;

public class SCRIPT : MonoBehaviour
{

    /// <summary>
    /// Ignacio
    /// Script basado en el video de youtube: https://www.youtube.com/watch?v=AqXC-QmhRXQ
    /// </summary>

    //explicare el codigo en comentarios de aquí en adelante


    [SerializeField] int ancho, altura; //hacen referencia a la altura y anchura maxima de la generación

    [SerializeField] int nBloquesMinimoEntrePiedraYPasto, nBloquesMaximoEntrePiedraYPasto; //este script divide en 3 terrenos: tierra, piedra, hierba
    /// La hierba se genera encima de todo y no tiene ninguna regla compleja alrededor de ello
    /// En cambio la piedra y tierra varian segunn los parametros de ancho, altura, nBloquesMinimoEntrePiedraYPasto y nBloquesMaximoEntrePiedraYPasto
    /// nBloquesMinimoEntrePiedraYPasto = cuales son los bloques minimos que existen entre la tierra y el pasto
    /// nBloquesMaximoEntrePiedraYPasto = cuales son los bloques maximos que existen entre la tierra y el pasto
    /// Todas estas variables sirven mayormente para la modificación entre que cantidad hay de tierra y piedra


    [SerializeField] int rangoAlturaMinima, rangoAlturaMaxima; //Estos son ajustes de los crecimientos o disminución del terreno
    /// rangoAlturaMinima = mientras mayor sea con respecto al rangoAlturaMaxima, el terreno ira en picada
    /// rangoAlturaMaxima = mientras mayor sea con respecto al rangoAlturaMinima, el terreno ira hacia arriba
    /// si son iguales, el terreno tendera a ser mas plano, si el maximo es mayor, el terreno ira hacia arriba, si el minimo es mayor, el terreno tendera hacia abajo 


    [SerializeField] GameObject tierra, piedra, pasto; //objetos utilizados para la generación


    void Start()
    {
        Generar();
    }

    void Generar()
    {
        for (int x = 0; x < ancho; x++)
        {
            int alturaMinima = altura - rangoAlturaMinima;
            int alturaMaxima = altura + rangoAlturaMaxima;

            altura = Random.Range(alturaMinima, alturaMaxima);

            int MinimaDistanciaAparicionPiedra = altura - nBloquesMinimoEntrePiedraYPasto;
            int MaximaDistanciaAparicionPiedra = altura - nBloquesMaximoEntrePiedraYPasto;

            int TotalDistanciaAparicionPiedra = Random.Range(MinimaDistanciaAparicionPiedra, MaximaDistanciaAparicionPiedra);

            for (int y = 0; y < altura; y++)
            {
                if(y < TotalDistanciaAparicionPiedra)
                {
                    aparecerObjeto(piedra, x, y);
                }
                else
                {
                    aparecerObjeto(tierra, x, y);
                }

                if(TotalDistanciaAparicionPiedra == altura)
                {
                    aparecerObjeto(piedra, x, altura);
                }
                else
                {
                    aparecerObjeto(pasto, x, altura);
                }


            }
        }
    }


    void aparecerObjeto(GameObject objeto, int anchoObjeto, int alturaObjeto)
    {
        GameObject instancia = Instantiate(objeto, new Vector2(anchoObjeto, alturaObjeto), Quaternion.identity);
        instancia.transform.parent = this.transform;
        if (objeto.name.ToLower().Contains("hierba") || objeto.name.ToLower().Contains("pasto"))
        {
            instancia.tag = "Hierba";
        }
    }

    void Update()
    {
        
    }
}
