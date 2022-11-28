using UnityEngine;

[CreateAssetMenu(fileName = "NewIAEnemy", menuName = "IAEnemy")]

public class IAEnemy : ScriptableObject
{
    [SerializeField] int dano = default;
    [SerializeField] float velocidade = default;
    [SerializeField] int distanciaPatrulha = default;
    [SerializeField] int distanciaAtaque = default;
    [SerializeField] float energia = default;

    public int Dano => dano;

}

