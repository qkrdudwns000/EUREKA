using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public bool isQuest = false;

    [SerializeField] private GameObject go_EnabelInteration;
    [SerializeField] private Animator anim;



    public void ZoneEnter()
    {
        anim.SetTrigger("Hello");
        go_EnabelInteration.SetActive(true);
    }
    public void Exit()
    {
        anim.SetTrigger("Hello");
        go_EnabelInteration.SetActive(false);
    }
    public void Interaction()
    {
        go_EnabelInteration.SetActive(false);
        GameManager.Inst.Action(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isQuest = true;
            ZoneEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isQuest = false;
            Exit();
        }
    }
}
