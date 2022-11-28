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
    [SerializeField] int dano = 5;
    public IAEnemy Inimigo;
    public Transform Casa;
    public int energia = default;
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
        agent.destination = RandomPosition(35);

        yield return new WaitForSeconds(1);
        if (Vector3.Distance(transform.position, target.transform.position) < AtaqueDistancia * 5)
        {
            StateMachine(States.persegue);
        }
        else
        if (UnityEngine.Random.value > 0.5)
        {
            StateMachine(States.para);
        }
        else
        {
            StateMachine(States.patrulha);
        }
    }


    IEnumerator EstadoPersegue()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        yield return new WaitForSeconds(0.1f);
        if (Vector3.Distance(transform.position, target.transform.position) < AtaqueDistancia)
        {
            StateMachine(States.ataca);
        }
        else
        if (Vector3.Distance(transform.position, target.transform.position) > AtaqueDistancia * 6)
        {
            StateMachine(state = States.para);
        }
        else
        {
            StateMachine(States.persegue);
        }
    }

    IEnumerator EstadoAtaque()
    {
        agent.isStopped = true;
        Debug.Log("tomou dano, valor:" + dano);
        energia -= 1;
        yield return new WaitForSeconds(0.1f);
        
        if (Vector3.Distance(transform.position, target.transform.position) > 4)
        {
            StateMachine(States.persegue);
        }
        else
        {

            StateMachine(States.ataca);
        }

        if(energia <= 0) 
        {
            StateMachine(States.descanca);
        }
    }

    IEnumerator EstadoParado()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        if (Vector3.Distance(transform.position, target.transform.position) < AtaqueDistancia * 5)
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
    }

   IEnumerator EstadoDescanco()
   {
       agent.destination = Casa.position;
        yield return new WaitForSeconds(5f);
        
        if(energia < 20)
        {
            StateMachine(States.descanca);
            energia += 5;
        }
        else
        {
            StateMachine(States.para);
        }


   }


}
