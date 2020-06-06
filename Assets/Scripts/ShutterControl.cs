using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//Código por Rodrigo Terpán Arenas. 

public class ShutterControl : MonoBehaviour
{//Esta función tiene como función principal es capturar la imagen de la cámara y generar la información correspondiente de los objetos que se encuentren en esta.

    //Variables que sirven para poder visualizar las bounding boxes en la interfaz de usuario de Unity (Para propósitos de prueba).
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    public Texture BoxTexture;

    //Inicializamos la cámara y una estructura tipo rect (Esquina inferior izquierda, ancho y alto).
    Camera cam;
    public Rect caja;

    //Variables que van a ser utilizadas a lo largo del código.
    int resWidth;
    int resHeight;
    public static int name;
    public GameObject instancer;
    private List<GameObject> instancercode;
    float centrox, centroy;
    string dir1, dir2, dir3;

    //Variable para obtener los puntos del colisionador.
    Vector2[] puntose;

    //Casillas para controlar el nombre y la dirección de guardado.
    public Toggle toglenombre, togledir;


    void Start()
    {
        if(toglenombre.isOn)
        {
            //Obtenemos el dato del número de imagen almacenado en player prefs.
            name = PlayerPrefs.GetInt("name", name);
        }
        else
        {
            PlayerPrefs.SetInt("name", 1);
            name = PlayerPrefs.GetInt("name", name);
            toglenombre.isOn = true;
        }
        
        puntose = new Vector2[8];
    }

    public static string ScreenShotName(int width, int height)
    {
        //Función que regresa la dirección en donde se van a guardar las imágenes generadas.

        //name++;
        string dir = PlayerPrefs.GetString("str2");
        //Debug.Log(dir);
        return (dir + "/"+ width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".png");
        //return "C:/Users/rodri/Desktop/Data/Screenshots/Train/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".png";
    }

    public static string ScreenShotNameVal(int width, int height)
    {
        //Función que regresa la dirección en donde se van a guardar las imágenes generadas.

        //name++;
        string dir = PlayerPrefs.GetString("str4");
        Debug.Log(dir.ToString());
        return (dir + "/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".png");
        //return "C:/Users/rodri/Desktop/Data/Screenshots/Val/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".png";
    }

    public static string TextFileName(int width, int height)
    {
        //Función que regresa la dirección en donde se van a guardar los archivos de texto generados.
        name++;
        string dir = PlayerPrefs.GetString("str3");
        //Debug.Log(dir);
        return (dir + "/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".txt");
        //return "C:/Users/rodri/Desktop/Data/Labels/Train/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".txt";
    }

    public static string TextFileNameVal(int width, int height)
    {
        //Función que regresa la dirección en donde se van a guardar los archivos de texto generados.
        name++;
        string dir = PlayerPrefs.GetString("str5");
        Debug.Log(dir.ToString());
        return (dir + "/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".txt");
        //return "C:/Users/rodri/Desktop/Data/Labels/Train/" + width.ToString() + "x" + height.ToString() + "_" + name.ToString() + ".txt";
    }


    public void Snap()
    {
        //Función que manda a llamar a la función para capturar la información.
        //En caso de que no haya objetos presentes en la escena, no se ejecuta la función.
        if (!toglenombre.isOn)
        {
            PlayerPrefs.SetInt("name", 0);
            name = PlayerPrefs.GetInt("name", name);
            toglenombre.isOn = true;
        }


        instancercode = instancer.GetComponent<CupInstanciate>().tally;
        if (instancercode.Count == 0)
            Debug.Log("No hay objetos para capturar");
        else
            takePics(instancercode);
    }

    public void takePics(List<GameObject> lista)
    {
        //Función principal para la captura de información:

        //Esta función se repite para cada objeto con la etiqueta cámara.  De momento sólo se realiza con una cámara.
        //Futuros desarrollos podrían ver incerementado el número de cámaras.
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Cam"))
        {
            Camera cam = go.GetComponent<Camera>();

            int num = lista.Count;

            //Creamos un arreglo de cadenas de caracteres para guardar los datos que vamos a registrar.
            string[] registro = new string[num];
            string temp;
            int contador = 0;
            foreach (GameObject x in lista)
            { //Esta función se repite por cada objeto en la lista que se registró en el código de instanciamiento.
                temp = "";
                registro[contador] = "";
                //Se crea un cuaternión de rotación con los datos del objeto.
                Quaternion rot = Quaternion.Euler(x.transform.rotation.eulerAngles);
                //Se genera una matriz de transformación para el objeto
                Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);

                //Se obtiene la posición en pantalla del centro del objeto (Este es uno de los datos que vamos a utilizar más adelante).
                Vector3 screenPos = cam.WorldToScreenPoint(x.transform.position);
                centrox = screenPos.x;
                centroy = screenPos.y;
                //Normalizamos los datos del centro del objeto con respecto a las dimensiones de la pantalla.
                centrox = centrox / Screen.width;
                centroy = (Screen.height-centroy) / Screen.height;

                //Debug.Log(centrox + "," + centroy);
                //Obtenemos la escala del objeto
                Vector3 escala = x.transform.localScale;

                //Obtenemos el colisionador cúbico del objeto.
                BoxCollider Colisionador = x.GetComponent<BoxCollider>();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Esta parte del código se utilizó para probar las operaciones sin necesitar de funciones externas al método principal.  Este pedazo de código se eliminará en el futuro.

                /*
                Vector4 posicion = Colisionador.transform.position;
                Vector4 puntomedio = Vector4.Scale(m*Colisionador.center,escala);
                //Debug.Log(posicion);
                //Debug.Log(puntomedio);
                Vector4 centro = posicion + puntomedio;
                //Debug.Log(centro);

                var puntos = new Vector2[]
                {
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, -Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, -Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, -Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, -Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
                cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, Colisionador.size.y, Colisionador.size.z) * 0.5f,escala)))
                };
                //Debug.Log(puntos[0] + "," + puntos[1] + "," + puntos[2] + "," + puntos[3] + "," + puntos[4] + "," + puntos[5] + "," + puntos[6] + "," + puntos[7]);
                var min = puntos[0];
                var max = puntos[0];
                foreach (Vector2 v in puntos)
                {
                    min = Vector2.Min(min, v);
                    max = Vector2.Max(max, v);
                }
                //Debug.Log(min + ", " + max);
                //Debug.Log(min.x + ", " + min.y + ", " + (max.x - min.x) + ", " + (max.y - min.y));
                */

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Vector3 posición = Colisionador.transform.position;

                //Obtenemos el boundingbox del objeto con la función caja e invertimos el eje y.
                if(CompruebaPared(Colisionador, m, escala, cam, x)==true)
                {
                    caja = Rectangulo(Colisionador, m, escala, cam);
                    float screenymin = Screen.height - caja.min.y;
                    float screenymax = Screen.height - caja.max.y;
                    Rect newpos = new Rect(caja.min.x, screenymin, caja.max.x - caja.min.x, screenymax - screenymin);

                    //Si el centro del objeto se encuentra dentro de la imagen, entonces se agregan los datos al arreglo de objetos a capturar.
                    if (0 < centrox && centrox < 1.0 && 0 < centroy && centroy < 1.0)
                    {
                        temp = x.tag.ToString() + " " + centrox.ToString() + " " + centroy.ToString() + " " + caja.width.ToString() + " " + caja.height.ToString();
                        //Debug.Log(temp);
                        registro[contador] = temp;
                        contador++;
                    }
                }
                
                //Debug.Log(registro[contador]);

            }
            
            resWidth = cam.pixelWidth;
            resHeight = cam.pixelHeight;

            //Obtenemos el ancho y el alto de la imagen y escribimos el documento txt con la función writetxt.
            if(togledir.isOn)
            {
                string textfilename = TextFileName(resWidth, resHeight);
                writetxt(textfilename, registro);
            }
            else
            {
                string textfilename = TextFileNameVal(resWidth, resHeight);
                writetxt(textfilename, registro);
            }


            //Creamos una nueva textura de renderización con 24 canales, la cual luego codificamos en un archivo png.
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            cam.Render();
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            RenderTexture.active = rt;
            screenShot.ReadPixels(cam.pixelRect, 0, 0);
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();

            //Guardamos esta imagen en la carpeta correspondiente.
            string filename = "";
            if(togledir.isOn)
            {
                filename = ScreenShotName(resWidth, resHeight);
            }

            else
            {
                filename = ScreenShotNameVal(resWidth, resHeight);
            }
            System.IO.File.WriteAllBytes(filename, bytes);

            //Apagamos las variables de renderización.
            cam.targetTexture = null;
            RenderTexture.active = null;
            rt.Release();

            //Guardamos el número de imagen para el siguiente ciclo.
            PlayerPrefs.SetInt("name", name);
            
        }
    }

    public Rect Rectangulo(BoxCollider Colisionador, Matrix4x4 m, Vector3 escala, Camera cam)
    {
        //Función para obtener la caja con el colisionador cúbico como dato de entrada.

        //Primero obtenemos la posición del centro del objeto y le sumamos la posición del ceontro del colisionador con respecto al objeto
        Vector4 posicion = Colisionador.transform.position;
        Vector4 puntomedio = Vector4.Scale(m * Colisionador.center, escala);
        //Debug.Log(posicion);
        //Debug.Log(puntomedio);
        Vector4 centro = posicion + puntomedio;
        //Debug.Log(centro);

        //En un struct de tipo Vector2, ponemos todas las esquinas del colisionador proyectadas al campo de visión.
        var puntos = new Vector2[]
        {
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, -Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, -Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, -Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, -Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
           cam.WorldToViewportPoint(centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, Colisionador.size.y, Colisionador.size.z) * 0.5f,escala)))
        };

        //Obtenemos los puntos máximos yb mínimos de entre todos los puntos x y y del struct puntos.
        puntose = puntos;
        var min = puntos[0];
        var max = puntos[0];
        foreach (Vector2 v in puntos)
        {
            min = Vector2.Min(min, v);
            max = Vector2.Max(max, v);
        }

        //Regresamos la estructura rect con la información del rectángulo compuesto entre los máximos y los mínimos.
        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
    }

    bool CompruebaPared(BoxCollider Colisionador, Matrix4x4 m, Vector3 escala, Camera cam, GameObject obj)
    {
        RaycastHit hit;
        int cont = 0;

        Vector4 posicion = Colisionador.transform.position;
        Vector4 puntomedio = Vector4.Scale(m * Colisionador.center, escala);
        Vector4 centro = posicion + puntomedio;

        Vector3 posfin = cam.transform.position;

        var puntos = new Vector3[]
       {
           (centro),
           (centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, -Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, -Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, -Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, -Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, Colisionador.size.y, -Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(Colisionador.size.x, Colisionador.size.y, Colisionador.size.z) * 0.5f,escala))),
           (centro + (m* Vector3.Scale(new Vector3(-Colisionador.size.x, Colisionador.size.y, Colisionador.size.z) * 0.5f,escala)))
       };

        foreach (Vector3 v in puntos)
        {
            Vector3 dir = posfin-v;
            int layerMask = 1 << 9;
            layerMask = ~layerMask;

            //Debug.DrawRay(v, dir, Color.green, 15f);
            if (Physics.Raycast(v,dir.normalized, out hit, dir.magnitude,layerMask) && hit.collider.tag != obj.tag)
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag != "Cam" && hit.collider.tag!=obj.tag)
                {  
                    cont++;
                }
            }
        }
        //Debug.Log(cont);
        if(cont>=4)
        {
            //Debug.Log("Falso");
            return false;
        }
        //SDebug.Log("Verdadero");    
        return true;
    }

    void writetxt(string direcc, string[] registro)
    {
        //Función para escribir los datos en un archivo txt.
        //La dirección en donde se va a guardar el archivo:
        string path = direcc;
        //Se inicia una función temporal writer para escribir todo lo que contiene el arreglo registro.
        StreamWriter writer = new StreamWriter(path, true);
        for (int i = 0; i < registro.Length; i++)
        {
            writer.WriteLine(registro[i]);
        }
        //Se termina la función writer.
        writer.Close();

    }
    void OnGUI()
    {
        //Función para visualizar el rectángulo en la interfaz de usuario.
        
        //Variables flotantes para invertir el eje y, debido a que el eje y en la interfaz de usuario de unity va en la dirección contraria al eje en la función de captura de imágenes.
        float screenymin = Screen.height - caja.min.y;
        float screenymax = Screen.height - caja.max.y;
        //Vector2 newmin = GUIUtility.ScreenToGUIPoint(position.min);
        //Vector2 newmax = GUIUtility.ScreenToGUIPoint(position.max);

        //Se crea una nueva variable rect con el nuevo eje y.
        Rect newpos = new Rect(caja.min.x, screenymin, caja.max.x - caja.min.x, screenymax - screenymin);
        //Se crea la textura y el estilo de interfaz de usuario para definir el rectángulo-
        if (_staticRectTexture == null)
        {
            _staticRectTexture = new Texture2D(1, 1);
        }

        if (_staticRectStyle == null)
        {
            _staticRectStyle = new GUIStyle();
        }
        Color newColor = new Color(0.3f, 0.4f, 0.6f, 0.3f);
        _staticRectTexture.SetPixel(0, 0, newColor);
        _staticRectTexture.Apply();

        _staticRectStyle.normal.background = _staticRectTexture;

        GUI.Box(newpos, GUIContent.none, _staticRectStyle);

        //Se dibujan los rectángulos sobre los objetos.
        GUI.Box(new Rect((int)puntose[0].x, Screen.height - (int)puntose[0].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[1].x, Screen.height - (int)puntose[1].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[2].x, Screen.height - (int)puntose[2].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[3].x, Screen.height - (int)puntose[3].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[4].x, Screen.height - (int)puntose[4].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[5].x, Screen.height - (int)puntose[5].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[6].x, Screen.height - (int)puntose[6].y, 1, 1), BoxTexture);
        GUI.Box(new Rect((int)puntose[7].x, Screen.height - (int)puntose[7].y, 1, 1), BoxTexture);
        
    }


}
