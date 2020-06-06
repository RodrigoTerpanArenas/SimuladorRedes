using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Código por Rodrigo Terpán Arenas.

public class Repeater : MonoBehaviour
{
    //Primero, declaramos todos los objetos en unity y los elementos en la interfaz de usuario que se van a usar.
    public InputField input, inputInst;
    int numero;
    public GameObject Luz1,Luz2,Cam1,Cam2,Zoom;
    Slider sliderluz1, sliderluz2, slidercam1, slidercam2, sliderzoom;
    public Toggle[] listaobj = new Toggle[14];
    public GameObject[] objazr = new GameObject[90];
    public Toggle toggle, toggleInst, toggleobst;
    public Button togglelisting, cupinstanciate, shuttercontrol,roominst,roomback,obsinst;
    public GameObject canvas;


    void Start()
    {
        //Inicializamos los sliders.
        sliderluz1 = Luz1.GetComponent<Slider>();
        sliderluz2 = Luz2.GetComponent<Slider>();
        slidercam1 = Cam1.GetComponent<Slider>();
        slidercam2 = Cam2.GetComponent<Slider>();
        sliderzoom = Zoom.GetComponent<Slider>();
    }

    public void ConsigueNumero()
    {
        //Esta función es para conseguir el número de repeticiones del método de captura
        if (input.text != "")
        {
            numero = int.Parse(input.text);
            //Debug.Log(numero);
            if (numero > 0)
            {
                //Es necesario relegar esto a una corrutina porque se necesita realizar pausas en la ejecuación del algoritmo.
                StartCoroutine(Repeticiones(numero));
            }
            else
            {
                Debug.Log("Escriba un número válido.");
            }
        }
        else
            Debug.Log("No hay nada aquí");
        
    }
    public int ConsigueNumeroInst(InputField input)
    {
        //Esta función es para conseguir el número de repeticiones del método de captura
        if (input.text != "")
        {
            numero = int.Parse(input.text);
            //Debug.Log(numero);
            if (numero > 0)
            {
                return numero;
            }
            else
            {
                Debug.Log("Escriba un número válido.");
                return 1;
            }
        }

        else
        {
            Debug.Log("Escriba un número válido.");
            return 1;
        }
        
    }

    IEnumerator Repeticiones(int numero)
    {
        //Este es el método  principal que realiza las repeticiones.
        /*Funciona de la siguiente forma:
         * 
         * 1.- Se obtienen todos los sliders apra controlar luz, dirección de cámara y zoom de la cámara (field of view).
         * 2.- Se borran las casillas previamente seleccionadas.
         * 3.-  Si la casilla de objetos al azar no está seleccionada, entonces se ejecuta la función de generar una lista para utilizar después.
         * 4.- Finalmente, la subrutina ejecuta las códigos ligados a los botones de generar objetos y de captura de pantalla.
         */
         if(!toggleInst.isOn)
        {
            canvas.GetComponent<CanvasGroup>().alpha = 0;
            for (int i = 0; i < numero; i++)
            {
                getSlider(sliderluz1);
                getSlider(sliderluz2);
                getSlider(slidercam1);
                getSlider(slidercam2);
                getSlider(sliderzoom);
                for (int a = 0; a < listaobj.Length; a++)
                {
                    listaobj[a].isOn = false;
                }
                if (toggle.isOn == false)
                {
                    generaLista();
                }
                yield return new WaitForSeconds(1);
                //togglelisting.onClick.Invoke();
                yield return new WaitForSeconds(2);
                cupinstanciate.onClick.Invoke();
                yield return new WaitForSeconds(1);
                if (toggleobst.isOn)
                {
                    int jok = Random.Range(1,10);
                    if (jok == 1)
                    { 
                        obsinst.onClick.Invoke();
                    }
                }
                yield return new WaitForSeconds(5);
                shuttercontrol.onClick.Invoke();
                yield return new WaitForSeconds(1);
                Resources.UnloadUnusedAssets();
            }
            canvas.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            int rep = ConsigueNumeroInst(inputInst);
            canvas.GetComponent<CanvasGroup>().alpha = 0;
            for (int i = 0; i < numero; i++)
            {
                getSlider(sliderluz1);
                getSlider(sliderluz2);
                getSlider(slidercam1);
                getSlider(slidercam2);
                getSlider(sliderzoom);
                if( i % rep == 0)
                {
                    roominst.onClick.Invoke();
                }
                for (int a = 0; a < listaobj.Length; a++)
                {
                    listaobj[a].isOn = false;
                }
                if (toggle.isOn == false)
                {
                    generaLista();
                }
                yield return new WaitForSeconds(1);
                //togglelisting.onClick.Invoke();
                yield return new WaitForSeconds(2);
                cupinstanciate.onClick.Invoke();
                yield return new WaitForSeconds(7);
                shuttercontrol.onClick.Invoke();
                yield return new WaitForSeconds(2);
            }
            canvas.GetComponent<CanvasGroup>().alpha = 1;
            roomback.onClick.Invoke();


        }

    }

    void getSlider(Slider slider)
    {
        //Función para generar el valor de un componente slider.
        //Se obtiene un valor al azar entre dos valores máximo y mínimo y se actualiza el slider con ese valor.
        float x = slider.maxValue;
        float y = slider.minValue;
        float valor = Random.Range(y, x);
        slider.value = valor;
    }
   
   void generaLista()
   {
        //Función para generar una lizta de números al azar entre 0 y 13 sin repeticiones.
        //Primero generamos un arreglo de números enteros.
        int[] lista = new int[14] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        //Luego se revuelve esta lista.
        shuffle(lista);
        //Se genera un número n al azar entre 1 y 13.
        int rango = Random.Range(1, lista.Length);
        //Se copian los primeros n números de la lista en un segundo arreglo de tamaño n.
        int[] lista2 = new int[rango];
        for (int i = 0; i < lista2.Length; i++)
        {
            lista2[i] = lista[i];
            //Se encienden las casillas correspondientes para su uso en otros códigos.
            listaobj[lista2[i]].isOn = true;
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
