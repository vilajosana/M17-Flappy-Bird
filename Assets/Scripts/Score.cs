using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona tiene el componente Player
        if (other.GetComponent<Player>() != null)
        {
            GameManager.Instance.IncreaseScore();
        }
    }
}
