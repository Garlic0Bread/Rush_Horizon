using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource coinFx;
    /*[SerializeField] int coinValue = 1;*/

    void OnTriggerEnter(Collider other)
    {
        coinFx.Play();
        this.gameObject.SetActive(false);
      /*  if (other.CompareTag("Player"))
        {
            coinFx.Play();
            ScoreManager scoreManager = findObjectOfType<ScoreManager>();

            if(scoreManager != null)
            {
                scoreManager.AddScore(coinValue);
            }
        }
        Destroy(gameObject);*/
    }
}
