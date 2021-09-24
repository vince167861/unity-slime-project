using UnityEngine;

public class L1_3_to_hidden_room : MonoBehaviour, IPortalHandler
{
  public Vector3 where = new Vector3(337, 49, 0);
  public void Handle()
  {
    Slime.instance.transform.position = where; //257,125
  }
}
