using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

//Código por Rodrigo Terpán Arenas.

public class ToogleListing : MonoBehaviour
{
    //Primero se declaran todos los objetos que va a manejar el código
    public GameObject[] objazr = new GameObject[90];
    public GameObject[] obj = new GameObject[14];
    public GameObject[] ext = new GameObject[5];
    Toggle[] toggles = new Toggle[14];
    public Toggle marcador;
    public Toggle obstaculos;
    GameObject Obj;
    public List<GameObject> tally,tallyobs;
    public List<int> reg;
    public List<int> notes, notes2;

    public void Generatelist()
    {
        //Función para generar lista
        if(marcador.isOn)
        {
            //Si está encendido la casilla de objetos al azar, se toma la lista de objetos de 90 elementos, se revuelve y se toma un número n de ellos para instanciar.
            int[] lista2 = new int[objazr.Length];
            for (int i = 0; i < lista2.Length; i++)
            {
             lista2[i] = i;
            }
            notes.Clear();
            shuffle(lista2);
            int rango = Random.Range(5, 20);
            int[] lista3 = new int[rango];
            for (int i = 0; i < lista3.Length; i++)
            {
            lista3[i] = lista2[i];
            }
            notes = lista3.ToList();
            tally.Clear();
            foreach (int x in notes)
            {
                tally.Add(objazr[x]);      
            }
        }
        else
        {
            //De lo contrario, se toma la lista de objetos del código repetidor y se crea la lista de objetos a instanciar con base en eso.
            toggles = GameObject.Find("RepeaterManager").GetComponent<Repeater>().listaobj;
            tally.Clear();
            int contador = 0;
            foreach (Toggle x in toggles)
            {
                if (x.isOn == true)
                {
                    //Debug.Log(obj[contador].name);
                    tally.Add(obj[contador]);
                }
                contador++;
            }
        }

        if(obstaculos.isOn)
        {
            int[] listao = new int[ext.Length];
            for (int i = 0; i < listao.Length; i++)
            {
                listao[i] = i;
            }
            notes2.Clear();
            shuffle(listao);
            int limit = listao.Length / 2;
            int rango = Random.Range(5, limit);
            int[] listax = new int[rango];
            for (int i = 0; i < listax.Length; i++)
            {
                listax[i] = listao[i];
            }
            notes2 = listax.ToList();
            tallyobs.Clear();
            foreach (int x in notes2)
            {
                tallyobs.Add(ext[x]);
            }
        }
        
    }

    void shuffle(int[] inicial)
    {
        //Utilizando el algoritmo de Knuth
        for (int t = 0; t < inicial.Length; t++)
        {
            int temp = inicial[t];
            int r = Random.Range(t, inicial.Length);
            inicial[t] = inicial[r];
            inicial[r] = temp;

        }
    }
}
