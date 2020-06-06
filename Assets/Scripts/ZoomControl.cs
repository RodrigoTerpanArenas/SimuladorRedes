using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Código por Rodrigo Terpán Arenas.
//Este código ya no está en uso por funcionalidad adicional de Unity.
public class ZoomControl : MonoBehaviour
{
    //Inicializamos el slider, la cámara y un valor flotante que va a cambiar.
    public Slider slider;
    private float ValorPrevio;
    public Camera cam;

    void Awake()
    {
        //Le agregamos al slider una bandera que indica si ha cambiado el valor del mismo.
        this.slider.onValueChanged.AddListener(this.OnSliderChanged);

        //Guardamos el valor previo del slider.
        this.ValorPrevio = this.slider.value;
    }

    //Función que se activa cuando cambia el valor del slider.
    void OnSliderChanged(float value)
    {
        //Actualizamos el valor de la profundidad de la cámara.
        //float delta = value - this.ValorPrevio;
        this.cam.fieldOfView = value;

        // Actualizamos el valor previo con el nuevo valor.
        this.ValorPrevio = value;
    }
   
}
