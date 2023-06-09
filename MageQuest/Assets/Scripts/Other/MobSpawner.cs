using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _mobs;
    [SerializeField] private Transform _mobPlace;

    private int _mobToSpawnIndex;
    private int _mobToSpawnPreviousIndex;

    private int _alreadySpawnedMobsAmount;
    private void Start()
    {
        _mobToSpawnPreviousIndex = _mobs.Length - 1; //������ ���������
        NewMobToSpawnIndex();
    }

    public void SpawnMob() //������� ������ ����
    {
        Instantiate(_mobs[_mobToSpawnIndex], _mobPlace);
        NewMobToSpawnIndex();
    }

    private void NewMobToSpawnIndex() //����� ���, ������� �����������
    {
        if (_alreadySpawnedMobsAmount >= 12)
        {
            _mobToSpawnIndex = Random.Range(0, _mobs.Length);
        }

        else
        {
            _mobToSpawnIndex = Random.Range(1, _mobs.Length); //������ ������
            _alreadySpawnedMobsAmount++;
        }

        if (_mobToSpawnIndex == _mobToSpawnPreviousIndex)
        {
            NewMobToSpawnIndex();
        }

        else
        {
            _mobToSpawnPreviousIndex = _mobToSpawnIndex;
        }
    }
}
