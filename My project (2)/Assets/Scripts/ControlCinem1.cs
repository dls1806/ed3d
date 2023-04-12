using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ControlCinem1 : MonoBehaviour
{
    public PlayableDirector directorCinem1;
    public PlayableDirector directorCinem2;
    float timeline1duration;
    float timeline2duration;
    [SerializeField] GameObject ballena;
    [SerializeField] GameObject barco;
    [SerializeField] GameObject personajeJugable;
    [SerializeField] GameObject personajeCinematica;
    [SerializeField] GameObject personajeCinematica2;
    [SerializeField] GameObject personaje2;
    bool cinem2vista;
    bool cinematica1finalizada;
    bool empezarContador;
    bool colision;
    [SerializeField] GameObject canvasfade;
    // Start is called before the first frame update
    void Start()
    {
        canvasfade.SetActive(false);
        colision = false;
        cinematica1finalizada = false;
        cinem2vista = false;
        directorCinem1.Play();
        timeline1duration = (float)directorCinem1.duration - 2;
        timeline2duration = (float)directorCinem2.duration;
        personajeJugable.SetActive(false);
        ballena.SetActive(true);
        barco.SetActive(true);
        personajeCinematica2.SetActive(false);
        personajeCinematica.SetActive(true);
        empezarContador = false;
    }

    // Update is called once per frame
    void Update()
    {

        timeline1duration -= Time.deltaTime;
        if(timeline1duration <= 0 && cinematica1finalizada == false)
        {
            cinematica1finalizada = true;
            finalizarCinematica1();
        }
        if(colision == true && cinem2vista == false && Input.GetKey(KeyCode.E))
        {
            cinem2vista = true;
            personajeCinematica2.SetActive(true);
            personajeJugable.SetActive(false);
            empezarCinematica2();
            empezarContador = true;
        }
        if (empezarContador == true)
        {
            timeline2duration -= Time.deltaTime;
        }
        if(timeline2duration <= 0)
        {
            finalizarCinematica2();
        }
    }
    void finalizarCinematica1()
    {
        //canvasfade.SetActive(true);
        personajeJugable.SetActive(true);
        ballena.SetActive(false);
        barco.SetActive(false);
        personajeCinematica.SetActive(false);
    }
    void empezarCinematica2()
    {
        directorCinem2.Play();
    }
    void finalizarCinematica2()
    {
        personajeJugable.SetActive(true);
        personajeCinematica2.SetActive(false);
        personaje2.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "cube")
        {
            colision = true;
        }
    }
}
