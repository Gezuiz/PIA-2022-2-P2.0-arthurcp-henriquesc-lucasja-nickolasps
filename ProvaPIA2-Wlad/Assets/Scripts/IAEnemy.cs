using UnityEngine;

[CreateAssetMenu(fileName = "NewIAEnemy", menuName = "IAEnemy")]

public class IAEnemy : ScriptableObject
{
    [SerializeField] int dano = default;
    [SerializeField] float velocidade = default;
    [SerializeField] int distanciaPatrulha = default;
    [SerializeField] int distanciaAtaque = default;
    [SerializeField] int energia = default;
    [SerializeField] int distanciaDesiste = default;

    public int Dano => dano;
    public float Velocidade => velocidade;
    public int DistanciaPatrulha => distanciaPatrulha;
    public int DistanciaAtaque => distanciaAtaque;
    public int Energia => energia;
    public int DistanciaDesiste => distanciaDesiste;


}

