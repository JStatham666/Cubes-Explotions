using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _renderer;
    private float _maxChanceCreate = 100f;

    public event Action<Cube> Dividing;
    public event Action<Cube> CubeRemoved;

    public Rigidbody CubeRigidbody { get; private set; }
    public float CurrentChanceCreate { get; private set; } = 100f;

    public void Init(float chanceCreate) => CurrentChanceCreate = chanceCreate;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() => _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    private void OnMouseDown() => Explode();

    private void Explode()
    {
        if (CanDivide())
            Dividing?.Invoke(this);

        CubeRemoved?.Invoke(this);
        Destroy(gameObject);

        Debug.Log(CurrentChanceCreate);
    }

    private bool CanDivide() => UnityEngine.Random.Range(0, _maxChanceCreate) < CurrentChanceCreate;
}