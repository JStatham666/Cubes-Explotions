using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explotionRadius;
    [SerializeField] private float _explotionForce;

    public void Explode(Rigidbody cube)
    {
        cube.AddExplosionForce(_explotionForce, cube.position, _explotionRadius);
    }
}