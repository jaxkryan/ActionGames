using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BossBattleManager : MonoBehaviour
{
    public GameObject rightBorderExit;
    public Transform bossSpawnPoint;
    public GameObject bossPrefab;
    public Slider bossHealthBar;

    public CinemachineVirtualCamera playerFollowCamera;
    public CinemachineVirtualCamera bossRoomCamera;

    private GameObject currentBoss;
    private Damageable bossDamageable;

    private void Start()
    {
        bossHealthBar.enabled = false;
        rightBorderExit.SetActive(true);
        SwitchToPlayerCamera();
    }

    public void StartBossBattle()
    {
        SpawnBoss();
        SwitchToBossRoomCamera();
    }

    private void SpawnBoss()
    {
        currentBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossDamageable = currentBoss.GetComponent<Damageable>();
        bossHealthBar.enabled = true;
        bossHealthBar.maxValue = bossDamageable.MaxHealth;
        bossHealthBar.value = bossDamageable.Health;

        bossDamageable.damageableDeath.AddListener(OnBossDeath);
        bossDamageable.damageableHit.AddListener(UpdateBossHealthBar);
    }

    private void UpdateBossHealthBar(int damage, Vector2 knockback)
    {
        bossHealthBar.value = bossDamageable.Health;
    }

    private void OnBossDeath()
    {
        rightBorderExit.SetActive(false);
        SwitchToPlayerCamera();
        Destroy(currentBoss);
    }

    private void SwitchToBossRoomCamera()
    {
        playerFollowCamera.Priority = 0;
        bossRoomCamera.Priority = 10;
    }

    private void SwitchToPlayerCamera()
    {
        bossRoomCamera.Priority = 0;
        playerFollowCamera.Priority = 10;
    }
}