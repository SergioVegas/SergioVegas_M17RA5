using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particles;

    private void OnTriggerEnter(Collider other) 
    { 
        if (other.CompareTag("Player")) 
        {
            if (particles.isPlaying)
            {
                Debug.Log("Stop");
                particles.Stop();
            }
            else
            {
                Debug.Log("Play");
                particles.Play();
            }
        }
    }
}
