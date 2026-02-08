using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ObtainWeapon : MonoBehaviour
{
    public static event Action InstateWeapon = delegate { };
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InstateWeapon.Invoke();
            Destroy(gameObject);
        }
    }
}
