using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int ItemType;
    Player playerScript;
    public float NoDamageTime;
    public AudioClip ItemTrigger;
    public float DownSpeed;

    public GameObject AddHP;
    public GameObject AddEngine;
    public GameObject AddLevel;
    public GameObject NoHIt;


    private void Update()
    {
        transform.position += Vector3.down * DownSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.SFXPlay("ItemTrigger", ItemTrigger);
            
            playerScript = collision.GetComponent<Player>();

            transform.position = new Vector3(1000, 1000, 1000);

            switch(ItemType)
            {
                case 0:
                    Instantiate(AddLevel, transform.position, Quaternion.identity);

                    if(playerScript.PlayerLevel >= 4)
                    {
                        playerScript.PlayerLevel = 4;
                        DataManager.CurScore += 30;
                        return;
                    }

                    playerScript.PlayerLevel += 1;

                    break;
                case 1:
                    Instantiate(NoHIt, transform.position, Quaternion.identity);

                    StartCoroutine(NoDamage());


                    break;

                case 2:
                    Instantiate(AddHP, transform.position, Quaternion.identity);

                    playerScript.HP.value += 10;

                    break;
                case 3:
                    Instantiate(AddEngine, transform.position, Quaternion.identity);

                    playerScript.Engine.value = playerScript.Engine.maxValue;

                    break;
            }
        }
    }

    IEnumerator NoDamage()
    {

        GameObject playerObj = GameObject.Find("Player");
        playerObj.layer = 31;
        yield return new WaitForSeconds(NoDamageTime);
        playerObj.layer = 6;

    }


}
