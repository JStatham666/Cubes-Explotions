using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private List<Cube> _cubes;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
        {
            cube.Dividing += Create;
            cube.Removing += Delete;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            cube.Dividing -= Create;
            cube.Removing -= Delete;
        }
    }

    private void Delete(Cube cube)
    {
        _cubes.Remove(cube);
    }

    private void Create(Cube explodedCube)
    {
        Cube cube = Instantiate(explodedCube, explodedCube.transform.position, Quaternion.identity);
        _cubes.Add(cube);
        cube.Dividing += Create;
        cube.Removing += Delete;
        cube.Init();

        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
        if (cubeRigidbody != null)
            _exploder.Explode(cubeRigidbody);
    }
}