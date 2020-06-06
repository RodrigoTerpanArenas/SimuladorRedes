using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Código por Rodrigo Terpán Arenas.
//Este código ya no está en uso por funcionalidad adicional de Unity.
public class RotateCam : MonoBehaviour
{
    public GameObject ObjetoRotar;
    public Slider slider;

    //Agregarmos una variable para guaradr el valor previo:
    private float ValorPrevio;

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

        //Rotamos el objeto por la diferencia del slider multiplicada por 360 (el valor del slider va de 0 a 1).
        float delta = value - this.ValorPrevio;
        this.ObjetoRotar.transform.Rotate(Vector3.up * delta * 360);
        //this.ValorPrevio.transform.Rotate(Vector3.right * delta * 360);

        // Actualizamos el valor previo con el nuevo valor.
        this.ValorPrevio = value;
    }

}
