using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

    private SavedValues sv;
    public GameObject _enemy;


    void Start()
    {
        sv = GameObject.FindObjectOfType<SavedValues>();
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", 10f, 10f);
    }

    void Spawn()
    {
        Vector3 pos;
        GameObject spawnpoint = null;
        if (sv.enemiesLength() < 4)
        {
            float l = 0;
            foreach (var point in sv.spawnpoints)
            {
                pos = point.transform.position;
                float tmp = sv.CalculatePathLength(sv.PlayerNav, pos);
                if (tmp > l)
                {
                    l = tmp;
                    spawnpoint = point;
                }
            }
            var la = Instantiate(_enemy, spawnpoint.transform.position, Quaternion.identity);
            sv.addEnemy((GameObject)la);
        }
        //     Debug.Log(spawnpoint.transform.position);
    }

}
