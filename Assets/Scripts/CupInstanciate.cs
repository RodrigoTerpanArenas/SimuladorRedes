using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código por Rodrigo Terpán Arenas.
//Este código sirve para hacer aparecer los modelos prefabricados en el entorno virtual.
public class CupInstanciate : MonoBehaviour
{

    //Primero, se inicializan las variables y objetos a usar en este código.
    GameObject Obj;
    Renderer rend;
    //Este par de variables son para una futura implementación de cambio de material de los objetos.
    public Color temp, temp2;
    public Material neoMat;
    public GameObject listas;
    public List<GameObject> reg, regobs;
    public List<GameObject> tally;
    public List<GameObject> tallyobs;
    //Inicializamos una variabl flotante para la función de raycasting que realizaremos más adelante.
    public float raylenght;
    float x_pos, z_pos, y_pos,x_ang,y_ang,z_ang;
    int contador;

    void Update()
    { /*
        //Función para cambiar el componente de color del material a un color con componente HSV aleatorio.
        //Este método no funciona para todos los objetos utilizados debido a que varios objetos utilizan un mapa de texturas, en ugar de tener colores definidos.
        //Serí necesario asegurar homogeneidad en los materiales de los objetos a generar, ya sea mediante la adquisición de objetos con características de color similares, o mediante la creación de los mismos.
        
        if (Input.GetKeyUp("n"))
        {
            if (tally.Count==10)
            {
                for (int i=0;1<10;i++)
                {
                    rend = tally[i].GetComponent<MeshRenderer> ();
                    rend.material = neoMat;  
                    rend.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
                }
            }
        }*/
    }

    public void instanciar()
    {
        //Si la lista temporal tiene un menor número de elementos que el arreglo de objetos, se ejecuta la función aparecer.
        //Si la lista tiene el mismo  número de elementos, primero se ejecuta la función desaparecer antes de la función aparecer.
        reg = listas.GetComponent<ToogleListing>().tally;
        regobs = listas.GetComponent<ToogleListing>().tallyobs;
        int num = reg.Count, numobs = regobs.Count;

        if (tally.Count < num)
        {
            Aparecer(reg, num);
        }
        else
        {
            Desaparecer(reg, num);
            Aparecer(reg, num);

        }
    }

    public void instanciarObs()
    {
        regobs = listas.GetComponent<ToogleListing>().tallyobs;
        int numobs = regobs.Count;

        if (tallyobs.Count == 0)
        {
            ApaObs(regobs, numobs);
        }
        else
        {
            DesObs(regobs, numobs);
            ApaObs(regobs, numobs);

        }
    }

    public void desapesp()
    {
        //Función únicamente pata desaparecer los objetos previamente instanciados.  Utilizada por el código repetidor.
        reg = listas.GetComponent<ToogleListing>().tally;
        int num = reg.Count;
        regobs = listas.GetComponent<ToogleListing>().tallyobs;
        int numobs = regobs.Count;
        if (tally.Count != 0)
        {
            Desaparecer(reg, num);
        }
        if (tallyobs.Count != 0)
        {
            DesObs(regobs, numobs);
        }


        /*if (tally.Count < num)
        {
            Aparecer(reg, num);
        }
        else
        {
            Desaparecer(reg, num);
            Aparecer(reg, num);

        }*/
    }

    void Aparecer(List<GameObject> registro, int n)
    {
        //Esta función instancía los objetos que se encuentran en la lista.
        //Primero genera posiciones y ángulos al azar, luego, con una función raycast, lanza un rayo en dirección y negativa.
        //Si el rayo colisiona con un objeto con etiqueta "table", se instancía el primer objeto de la lista en esa posición.
        // Se repite esta función con todos los otros elementos de la lista.
    int contador = 0;
        while (contador < n)
        {
            RaycastHit hit;
            x_pos = Random.Range(-80.0f, 80.0f);
            z_pos = Random.Range(-40f, 40f);
            y_pos = transform.position.y;
            x_ang = Random.Range(0.0f, 359.0f);
            y_ang = Random.Range(0.0f, 359.0f);
            z_ang = Random.Range(0.0f, 359.0f);

            Vector3 inicio = new Vector3(x_pos, y_pos, z_pos);
            Ray probe = new Ray(inicio, Vector3.down);

            if (Physics.Raycast(probe, out hit))
            {
                if (hit.collider.tag == "Table")
                {
                    float numero = Random.Range(0.8f,1.2f);

                    Vector3 deployment = new Vector3(x_pos, y_pos - hit.distance + 20.0f, z_pos);
                    Obj = Instantiate(registro[contador], deployment, Quaternion.Euler(new Vector3(0, y_ang, 0)));
                    Obj.transform.localScale = Obj.transform.localScale * numero;
                    tally.Add(Obj);
                    contador++;
                }
            }
        }
    }

    void Desaparecer(List<GameObject> registro, int n)
    {
        //La función desaparecer, simplemente destruye los objetos instanciados que se encuentren en la lista tally en orden descendente.  
        //Posteriormente, vacía la lista.
        if(n>0)
        {
            int contador = n;
            do
            {
                Obj = tally[contador - 1];
                Destroy(Obj);
                contador--;
            } while (contador > 0);
            tally.Clear();
        }
        
    }

    void ApaObs(List<GameObject> registro, int n)
    {
        int contador = 0;
        while (contador < n)
        {
            RaycastHit hit;
            x_pos = Random.Range(-80.0f, 80.0f);
            z_pos = Random.Range(-40f, 40f);
            y_pos = transform.position.y;
            x_ang = Random.Range(0.0f, 359.0f);
            y_ang = Random.Range(0.0f, 359.0f);
            z_ang = Random.Range(0.0f, 359.0f);

            Vector3 inicio = new Vector3(x_pos, y_pos, z_pos);
            Ray probe = new Ray(inicio, Vector3.down);

            if (Physics.Raycast(probe, out hit))
            {
                if (hit.collider.tag == "Table")
                {
                    float numero = Random.Range(0.8f, 1.2f);

                    Vector3 deployment = new Vector3(x_pos, y_pos - hit.distance + 20.0f, z_pos);
                    Obj = Instantiate(registro[contador], deployment, Quaternion.Euler(new Vector3(0, y_ang, 0)));
                    Obj.transform.localScale = Obj.transform.localScale * numero;
                    tallyobs.Add(Obj);
                    contador++;
                }
            }
        }
    }

    void DesObs(List<GameObject> registro, int n)
    {
        //La función desaparecer, simplemente destruye los objetos instanciados que se encuentren en la lista tally en orden descendente.  
        //Posteriormente, vacía la lista.
        if (n > 0)
        {
            //Debug.Log(registro);
            //Debug.Log(n);
            int contador = n;
            do
            {
                Obj = tallyobs[contador - 1];
                Destroy(Obj);
                contador--;
            } while (contador > 0);
            tallyobs.Clear();
        }
    }
}
