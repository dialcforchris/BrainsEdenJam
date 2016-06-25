using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _col)
    {
        _col.GetComponent<Player>().Die();
    }
}
