using UnityEngine;
using System.Collections.Generic;

public class Grama : MonoBehaviour
{
    [Header("L-System Settings")]
    public string axiom = "F";
    public int iterations = 4;
    public float angle = 25f;
    public float length = 5f;

    [Header("Cantidad de plantas")]
    public int cantidadPlantas = 5;

    [Header("Prefab de la planta (este mismo script)")]
    public GameObject plantaPrefab;
    [HideInInspector]
    public string reglaAleatoriaActual = "FF+[+F-F-F]-[-F+F+F]"; // Valor por defecto

    private string currentString;
    private Dictionary<char, string> rules = new Dictionary<char, string>();

    void Start()
    {
        if (plantaPrefab != null && gameObject.name == "SembradorGrama")
        {
            StartCoroutine(EsperarYCrearPlantas());
            return;
        }

        // Solo genera el árbol si no es un clon instanciado por el sembrador
    }

    public System.Collections.IEnumerator EsperarYCrearPlantas()
    {
        yield return new WaitForSeconds(0.1f);
        CrearPlantasSobreHierba();
        Destroy(gameObject);
    }

    void CrearPlantasSobreHierba()
    {
        GameObject[] bloquesHierba = GameObject.FindGameObjectsWithTag("Hierba");
        Debug.Log("Bloques de hierba encontrados: " + bloquesHierba.Length);
        if (bloquesHierba.Length == 0) return;

        // Selecciona posiciones aleatorias sin repetir
        List<int> indices = new List<int>();
        for (int i = 0; i < bloquesHierba.Length; i++) indices.Add(i);
        for (int i = 0; i < cantidadPlantas && indices.Count > 0; i++)
        {
            int idx = Random.Range(0, indices.Count);
            GameObject bloque = bloquesHierba[indices[idx]];
            indices.RemoveAt(idx);

            Vector3 pos = bloque.transform.position + Vector3.up * 0.5f;
            GameObject nuevaPlanta = Instantiate(plantaPrefab, pos, Quaternion.identity);
            nuevaPlanta.tag = "PlantaLSystem";
            Grama grama = nuevaPlanta.GetComponent<Grama>();
            if (grama != null)
            {
                grama.InicializarYGenerar(axiom, iterations, angle, length, reglaAleatoriaActual);
            }
        }
    }

    string GenerateLSystem(string input, int iter)
    {
        string output = input;
        for (int i = 0; i < iter; i++)
        {
            string newString = "";
            foreach (char c in output)
            {
                if (rules.ContainsKey(c))
                    newString += rules[c];
                else
                    newString += c.ToString();
            }
            output = newString;
        }
        return output;
    }

    void DrawLSystem(string instructions)
    {
        Stack<Vector3> positionStack = new Stack<Vector3>();
        Stack<Quaternion> rotationStack = new Stack<Quaternion>();
        Vector3 position = transform.position;
        Quaternion rotation = Quaternion.identity;

        foreach (char c in instructions)
        {
            if (c == 'F')
            {
                Vector3 start = position;
                position += rotation * Vector3.up * length;
                LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
                lineRenderer.gameObject.tag = "Line";
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, position);
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startColor = Color.yellow;
                lineRenderer.endColor = Color.red;

            }
            else if (c == '+')
            {
                rotation *= Quaternion.Euler(0, 0, angle);
            }
            else if (c == '-')
            {
                rotation *= Quaternion.Euler(0, 0, -angle);
            }
            else if (c == '[')
            {
                positionStack.Push(position);
                rotationStack.Push(rotation);
            }
            else if (c == ']')
            {
                position = positionStack.Pop();
                rotation = rotationStack.Pop();
            }
        }
    }

    public void InicializarYGenerar(string axiom, int iterations, float angle, float length, string reglaF)
    {
        this.axiom = axiom;
        this.iterations = iterations;
        this.angle = angle;
        this.length = length;

        rules.Clear();
        rules.Add('F', reglaF);
        currentString = GenerateLSystem(this.axiom, this.iterations);
        DrawLSystem(currentString);
    }

    public string GenerarReglaAleatoria()
    {
        
        string[] opciones = { "F", "F+F", "F-F", "F[+F]F[-F]F", "F[+F-F]F", "F[-F+F]F", "FF", "F[+F]F", "F[-F]F" };
        int n = Random.Range(2, 10); // Número de segmentos en la regla
        string regla = "";
        for (int i = 0; i < n; i++)
        {
            regla += opciones[Random.Range(0, opciones.Length)];
            if (Random.value > 0.7f) regla += "+"; // Añade giro ocasional
            if (Random.value > 0.7f) regla += "-"; // Añade giro ocasional
        }
        return regla;
    }
}
