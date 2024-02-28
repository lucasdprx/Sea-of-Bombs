using UnityEngine;

public class BOMB : MonoBehaviour
{
    public GameObject bombObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        GameObject bal = Instantiate(bombObject, gameObject.transform.position, Quaternion.identity);
        bal.transform.localScale = new Vector3(2 ,2 ,2);
    }
}
