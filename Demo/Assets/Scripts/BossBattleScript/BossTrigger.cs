using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private BossBattleManager bossBattleManager;

    private void Start()
    {
        bossBattleManager = GetComponentInParent<BossBattleManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossBattleManager.StartBossBattle();
            gameObject.SetActive(false);
        }
    }
}