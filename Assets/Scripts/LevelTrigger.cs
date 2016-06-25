using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.tag == "Player")
        {
            if (_col.GetComponent<Player>().playerState != Player.PlayerStates.DEAD)
            StartCoroutine(_col.GetComponent<Player>().Die());
        }
    }
}
