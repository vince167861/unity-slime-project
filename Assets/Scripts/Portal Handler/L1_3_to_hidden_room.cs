using UnityEngine;

public class L1_3_to_hidden_room : MonoBehaviour, IPortalHandler
{
  public void Handle()
  {
    Slime.instance.transform.position = new Vector3(0, 0, 0);
  }
}
