using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minCreate = 2;
    [SerializeField] private int _maxCreate = 6;
    [SerializeField] private int _chanceDivider = 2;
    [SerializeField] private int _scaleDivider = 2;

    public float CurrentChanceCreate { get; private set; } = 100f;
    private float _maxChanceCreate = 100f;

    public event Action<Cube> Dividing;
    public event Action<Cube> Removing;

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void OnMouseDown()
    {
        Explode();
        Destroy(gameObject);
    }

    public void Init(float chanceCreate)
    {
        transform.localScale /= _scaleDivider;
        CurrentChanceCreate = chanceCreate / _chanceDivider;
    }

    private void Explode()
    {
        Removing?.Invoke(this);

        if (CanDivide())
        {
            int amountOfCubes = UnityEngine.Random.Range(_minCreate, _maxCreate + 1);

            for (int i = 0; i < amountOfCubes; i++)
            {
                Dividing?.Invoke(this);
            }
        }
    }

    private bool CanDivide()
    {
        return UnityEngine.Random.Range(0, _maxChanceCreate) < CurrentChanceCreate;
    }
}