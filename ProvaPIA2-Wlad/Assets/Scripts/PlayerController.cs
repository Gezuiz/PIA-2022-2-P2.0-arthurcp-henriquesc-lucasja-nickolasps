using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _velocidadeFrente;
    float _velocidadeTras;
    float _velocidadeGirar;

    //Metodo Start e executado uma unica vez, quando o script e executado.
    void Start()
    {
        _velocidadeFrente = 10;
        _velocidadeTras = 5;
        _velocidadeGirar = 60;
    }

    //Medodo Update e executado a cada Frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(0, 0, (_velocidadeFrente * Time.deltaTime));
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(0, 0, (-_velocidadeTras * Time.deltaTime));
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, (-_velocidadeGirar * Time.deltaTime), 0);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, (_velocidadeGirar * Time.deltaTime), 0);
        }
    }
}

