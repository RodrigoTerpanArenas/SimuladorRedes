using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código por Rodrigo Terpán Arenas.
//Este código ya no está en uso por funcionalidad adicional de Unity.
public class LightControl : MonoBehaviour
{
    //Inicializamos los objetos que se van a utilizar, las luces que se van a cambiar y un par de valores flotantes.
    public GameObject Faro1, Faro2;
    Light light1, light2;
    float val1, val2;

    
    void Start()
    {
        //Obtenemos las luces de los objetos.
        light1 = Faro1.GetComponent<Light>();
        light2 = Faro2.GetComponent<Light>();
    }

   
    void Update()
    {
        //Cada vez que se presiona la tecla l, el valor de la intensidad de las luces cambia a un valor flotante dentro de un rango.

        /*
     if(Input.GetKeyUp("l"))
        {
            light1.intensity = Random.Range(0.0f,6.0f);
        }

     if(Input.GetKeyUp("p"))
        {
            light2.intensity = Random.Range(0.0f, 1.0f);
        }*/
    }
}
