using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        
    }

    public void setColor(string cor)
    {
        RawImage imagem = this.gameObject.GetComponent<RawImage>();
        switch (cor)
        {
            case ("red"):
                imagem.color = Color.red;
                break;
            case ("green"):
                imagem.color = Color.green;
                break;
            case ("white"):
                imagem.color = Color.white;
                break;
            case ("blue"):
                imagem.color = Color.blue;
                break;
        }
    }

}
