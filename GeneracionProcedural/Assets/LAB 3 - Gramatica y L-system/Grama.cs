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

    private string currentString;
    private Dictionary<char, string> rules = new Dictionary<char, string>();

    void Start()
    {
        if (plantaPrefab != null)
        {
            StartCoroutine(EsperarYCrearPlantas());
            return;
        }

        // L-system normal (los clones dibujan la planta)
        rules.Add('F', "FF+[+F-F-F]-[-F+F+F]");
        currentString = GenerateLSystem(axiom, iterations);
        DrawLSystem(currentString);
    }

    System.Collections.IEnumerator EsperarYCrearPlantas()
    {
        // Espera unos frames para que el terreno se genere
        yield return new WaitForSeconds(0.1f); // Puedes ajustar el tiempo si es necesario

        CrearPlantasSobreHierba();
        Destroy(this); // El sembrador no dibuja planta
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

            Vector3 pos = bloque.transform.position + Vector3.up * 0.5f; // Ajusta si es necesario
            Instantiate(plantaPrefab, pos, Quaternion.identity);
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
                Debug.DrawLine(start, position, Color.green, 100f, false);
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
}
