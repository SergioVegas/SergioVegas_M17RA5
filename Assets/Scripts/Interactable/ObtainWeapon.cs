using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ObtainWeapon : MonoBehaviour, IInteractable
{
    public static event Action InstateWeapon = delegate { };
    private bool _instateWeapon = false;
    public void Interact()
    {
        if(!_instateWeapon)
        {
            InstateWeapon.Invoke();
            Destroy(gameObject);
            _instateWeapon = true;
        }
    }
}
