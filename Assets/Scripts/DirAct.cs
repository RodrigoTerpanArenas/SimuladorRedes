using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;

public class DirAct : MonoBehaviour
{ 
    //primero nombramos a las variables que necesitamos para actualizar los strings:
    public InputField in1;
    string dir1;
    public string dir2, dir3, dirv2, dirv3;

    private void Awake()
    {
        dir1 = PlayerPrefs.GetString("str1", "C:/Users/rodri/Desktop");
        in1.GetComponent<InputField>().placeholder.GetComponent<Text>().text = dir1;
        crear(dir1);
        dirv2 = PlayerPrefs.GetString("str4", "C:/Users/rodri/Desktop/Data/Screenshots/Val");
        dirv3 = PlayerPrefs.GetString("str5", "C:/Users/rodri/Desktop/Data/Labels/Val");
        dir2 = PlayerPrefs.GetString("str2", "C:/Users/rodri/Desktop/Data/Screenshots/Train");
        dir3 = PlayerPrefs.GetString("str3", "C:/Users/rodri/Desktop/Data/Labels/Train");
        
        if(PlayerPrefs.HasKey("str4"))
        {
            Debug.Log(dir2);
        }
        if (PlayerPrefs.HasKey("str5"))
        {
            Debug.Log(dir3);
        }

        PlayerPrefs.SetString("str1", dir1);
        PlayerPrefs.SetString("str2", dir2);
        PlayerPrefs.SetString("str3", dir3);
        PlayerPrefs.SetString("str4", dirv2);
        PlayerPrefs.SetString("str5", dirv3);       
    }

    public void actualizar()
    {
        if(in1.text!="")
        {
            dir1 = in1.text.ToString();
            if (IsValidPath(dir1) == true)
            {
                //Debug.Log(dir1);
                crear(dir1);
                dir2 = dir1 + "/Data/Screenshots/Train";
                dir3 = dir1 + "/Data/Labels/Train";
                dirv2 = dir1 + "/Data/Screenshots/Val";
                dirv3 = dir1 + "/Data/Labels/Val";
                in1.GetComponent<InputField>().placeholder.GetComponent<Text>().text = dir1;
            }
            else
            {
                dir1 = in1.GetComponent<InputField>().placeholder.GetComponent<Text>().text;
                crear(dir1);
                dir2 = dir1 + "/Data/Screenshots/Train";
                dir3 = dir1 + "/Data/Labels/Train";
                dirv2 = dir1 + "/Data/Screenshots/Val";
                dirv3 = dir1 + "/Data/Labels/Val";
            }
            PlayerPrefs.SetString("str1", dir1);
            PlayerPrefs.SetString("str2", dir2);
            PlayerPrefs.SetString("str3", dir3);
            PlayerPrefs.SetString("str4", dirv2);
            PlayerPrefs.SetString("str5", dirv3);
        }
    }

    //Método para verificar si la dirección escrita es válida.
    private bool IsValidPath(string path)
    {
        path.Replace('/','\'');
        Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
        if (!driveCheck.IsMatch(path.Substring(0, 3))) return false;
        string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
        strTheseAreInvalidFileNameChars += @":/?*" + "\"";
        Regex containsABadCharacter = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
        if (containsABadCharacter.IsMatch(path.Substring(3, path.Length - 3)))
            return false;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return true;
    }

    void crear(string dir1)
    {
        if (!Directory.Exists(dir1 + "/Data"))
        {
            Directory.CreateDirectory(dir1 + "/Data");
            Directory.CreateDirectory(dir1 + "/Data/Screenshots/Train");
            Directory.CreateDirectory(dir1 + "/Data/Labels/Train");
            Directory.CreateDirectory(dir1 + "/Data/Screenshots/Val");
            Directory.CreateDirectory(dir1 + "/Data/Labels/Val");
        }
        if (!Directory.Exists(dir1 + "/Data/Screenshots/Train"))
        {
            Directory.CreateDirectory(dir1 + "/Data/Screenshots/Train");
        }
        if (!Directory.Exists(dir1 + "/Data/Labels/Train"))
        {
            Directory.CreateDirectory(dir1 + "/Data/Labels/Train");
        }
        if (!Directory.Exists(dir1 + "/Data/Screenshots/Val"))
        {
            Directory.CreateDirectory(dir1 + "/Data/Screenshots/Val");
        }
        if (!Directory.Exists(dir1 + "/Data/Labels/Val"))
        {
            Directory.CreateDirectory(dir1 + "/Data/Labels/Val");
        }
    }
}

