using UnityEngine;

public class RandomLighting : MonoBehaviour
{
    public GameObject Lightning;
    
    void Update()
    {
        if (Random.Range(0,100) == 50)
        {
            Vector3 positionA = new Vector3(Random.Range(-10,10), 5, 0);
            Quaternion rotationA = Quaternion.Euler(180, 0, Random.Range(-20, 21));
            Instantiate(Lightning, positionA, rotationA);
        }
    }
}
