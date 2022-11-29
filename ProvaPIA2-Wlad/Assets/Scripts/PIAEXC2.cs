using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PIAEXC2 : MonoBehaviour
{
    #region Variáveis
    public GameObject target;
    public NavMeshAgent agent;
    [SerializeField] float AtaqueDistancia = 3;
    [SerializeField] int dano = default;
    public IAEnemy Inimigo;
    public Transform Casa;
    public int energia = default;
    public int energiaMAX = default;
    #endregion
    public enum States
    {
        persegue,
        ataca,
        para,
        patrulha,
        descanca,
    }

    public States state;

    // Start is called before the first frame update
    void Start()
    {
        agent.speed = Inimigo.Velocidade;
        dano = Inimigo.Dano;
        energia = Inimigo.Energia;
        energiaMAX = energia;
        
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        StartCoroutine("EstadoParado");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void StateMachine(States _state)
    {
        state = _state;
        switch (state)
        {
            case States.persegue:
                StartCoroutine("EstadoPersegue");
                break;
            case States.ataca:
                StartCoroutine("EstadoAtaque");
                break;
            case States.para:
                StartCoroutine("EstadoParado");
                break;
            case States.patrulha:
                StartCoroutine("EstadoPatrulha");
                break;
            case States.descanca:
                StartCoroutine("EstadoDescanco");
                break;
        }
    }
    Vector3 RandomPosition(float range)
    {
        Vector3 pos;
        pos = transform.position + new Vector3(UnityEngine.Random.Range(-range, range)
            , 0
            , UnityEngine.Random.Range(-range, range));
        return pos;
    }
    private IEnumerator EstadoPatrulha()
    {
        agent.isStopped = false;
        agent.destination = RandomPosition(15);
        Debug.Log("salve");
        yield return new WaitForSeconds(1);
        if (Vector3.Distance(transform.position, target.transform.position) < AtaqueDistancia * Inimigo.DistanciaPatrulha)
        {
            StateMachine(States.persegue);
        }
        else
        {
            StateMachine(States.patrulha);
        }
        
        if (energia < energiaMAX)
        {
            StateMachine(States.descanca);
        }
    }


    IEnumerator EstadoPersegue()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        
        yield return new WaitForSecondsRealtime(0.1f);
        if (Vector3.Distance(transform.position, target.transform.position) < AtaqueDistancia)
        {
            StateMachine(States.ataca);
        }
        else
        if (Vector3.Distance(transform.position, target.transform.position) > AtaqueDistancia * Inimigo.DistanciaDesiste)
        {
            StateMachine(state = States.para);
        }
        else
        {
            StateMachine(States.persegue);
        }
        if (energia < energiaMAX)
        {
            StateMachine(States.descanca);
        }
    }

    IEnumerator EstadoAtaque()
    {
        agent.isStopped = true;
        Debug.Log("tomou dano, valor:" + dano);
        energia -= 1;
        yield return new WaitForSecondsRealtime(0.5f);

        if(energia > 0)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > Inimigo.DistanciaAtaque)
            {
                StateMachine(States.persegue);
            }
            else
            {

                StateMachine(States.ataca);
            }
        }
        else
        {
            StateMachine(States.descanca);
            energia = 0;
        }
       
        
    }

    IEnumerator EstadoParado()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        
        if (Vector3.Distance(transform.position, target.transform.position) < AtaqueDistancia * Inimigo.DistanciaPatrulha)
        {
            StateMachine(States.persegue);
        }
        else
        if (UnityEngine.Random.value > 0.5)
        {
            StateMachine(States.patrulha);

        }
        else
        {
            StateMachine(States.para);
        }
        if (energia < energiaMAX)
        {
            StateMachine(States.descanca);
        }
    }

   IEnumerator EstadoDescanco()
   {
        agent.isStopped = false;
        agent.destination = Casa.position;

        yield return new WaitForSecondsRealtime(2f);
        
        while(energia < energiaMAX)
        {
            StateMachine(States.descanca);
            energia += 2;
        }
        
        if(energia >= energiaMAX)
        {
            StateMachine(States.patrulha);
        }
   }
}
