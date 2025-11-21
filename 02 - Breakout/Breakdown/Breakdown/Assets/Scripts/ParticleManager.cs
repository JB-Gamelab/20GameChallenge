using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float deathTimer = 5.0f;

    private void Update()
    {
        if (deathTimer > 0)
        {
            deathTimer = deathTimer - 0.1f * Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }
    }
}
