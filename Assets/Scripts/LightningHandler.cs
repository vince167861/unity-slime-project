using UnityEngine;

public class LightningHandler : MonoBehaviour
{
    float age = 0;
    public float ttl = 0.5f;

    void Update()
    {
        age += Time.deltaTime;
        if (age >= ttl || Game.storyEffect == Game.StoryEffect.Clear) Destroy(gameObject);
    }
}
