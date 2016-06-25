using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.tag == "Player")
        {
            _col.GetComponent<Player>().Die();
        }
    }
}
