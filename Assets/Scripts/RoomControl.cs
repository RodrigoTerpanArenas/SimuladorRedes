using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject[] prev = new GameObject[16];
    public GameObject[] act = new GameObject[5];
    public GameObject[] Obstacles = new GameObject[0];
    public GameObject Mesa;
    GameObject Obstacle, Obj;
    public List<GameObject> tallymobel;
    public int maxSpawnAttemptsPerObstacle = 10;
    int num1, num2, num3, num4, num5 = 1, ObsACol;

    void Start()
    {
        
    }

    public void create()
    {
        Desaparecer();
        int num = tallymobel.Count;
        if (num != 0)
        {
            Borrar(tallymobel, num);
        }
        //Esta función sirve para el instanciamiento de los cuartos.
        //Vamos a tomar los cuatro tipos de objetos que tenemos (sin contar las paredes) y vamos a instanciarlos en ciertas cantidades al azar.
        //La única excepción es la mesa central, que siempre se va a mantener en el centro del cuarto.

        num1 = Random.Range(1, 4);
        num2 = Random.Range(1, 4);
        num3 = Random.Range(1, 4);
        num4 = Random.Range(1, 4);
        ObsACol = num1 + num2 + num3 + num4 + num5;

        for(int i = 0; i< ObsACol; i++)
        {
            if(i<num1)
            {
                Obstacle = act[0];
            }
            else if(i>=num1 && i<num1+num2)
            {
                Obstacle = act[1];
            }
            else if (i >= num1+num2 && i < num1 + num2 + num3)
            {
                Obstacle = act[2];
            }
            else if (i >= num1 + num2 + num3 && i < num1 + num2 + num3 + num4)
            {
                Obstacle = act[3];
            }
            else
            {
                Obstacle = act[4];
            }

            Vector3 posición = Vector3.zero;

            bool PosVal = false;

            int Attempts = 0;

            while(!PosVal && Attempts<maxSpawnAttemptsPerObstacle)
            {
                Attempts++;
                Vector3 escala = Obstacle.transform.localScale;
                BoxCollider box = Obstacle.GetComponent<BoxCollider>();
                Vector3 size = box.size;

                posición = new Vector3(Random.Range(-350.0f, 350.0f), 0, Random.Range(-350.0f, 350.0f));
                PosVal = true;
                Collider[] colliders = Physics.OverlapBox(posición, Vector3.Scale(size, escala));

                foreach(Collider col in colliders)
                {
                    if(col.tag == "Cam" || col.tag == "Inv" || col.tag == "Table")
                    {
                        PosVal = false;
                    }
                }
            }
            if(PosVal == true)
            {
                var clone = Instantiate(Obstacle, posición /*+ Obstacle.transform.position*/, Quaternion.identity);
                tallymobel.Add(clone);
            }
            
        }

    }  

    public void erase()
    {
        //Esta función va a ser para regresar los muebles a la normalidad y eliminar los muebles instanciados.
        int num = tallymobel.Count;
        if (num != 0)
        {
            Borrar(tallymobel, num);
        }
        Aparecer();
    }

    //Estas dos funciones son para desaparecer y aparecer los objetos presentes al principio de la escena.
    void Desaparecer()
    {
        foreach(GameObject go in prev)
        {
            go.SetActive(false);
        }
    }

    void Aparecer()
    {
        foreach (GameObject go in prev)
        {
            go.SetActive(true);
        }
    }

    void Borrar(List<GameObject> registro, int n)
    {
        //La función borrar, simplemente destruye los objetos instanciados que se encuentren en la lista tally en orden descendente.  
        //Posteriormente, vacía la lista.
        if (n > 0)
        {
            int contador = n;
            do
            {
                Obj = tallymobel[contador - 1];
                Destroy(Obj);
                contador--;
            } while (contador > 0);
            tallymobel.Clear();
        }

    }
}
