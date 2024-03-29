using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ForWoodcutter))]
public class Interactable : MonoBehaviour
{
    // bool canInteract = true;
    // bool itemState = false;

    [SerializeField] bool canBeCarried = false;
    [SerializeField] SpriteRenderer rootsOverlay = null;
    [SerializeField] Animation rootsAnimation;
    [SerializeField] AudioClip laughSound;
    [SerializeField] UIHandler uiHandler;




    public void Start()
    {
        uiHandler = GameObject.FindGameObjectWithTag("UIHandler").GetComponent<UIHandler>();
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyUp(KeyCode.F))
        {
            Interact(other);
        }

        else return;

    }

    public void Interact(Collider other)
    {
        if (other.GetComponent<PlayerController>().carriedItem == null && this.canBeCarried)
        {
            PickUp(other);
        }

        else
        {
            if (!this.GetComponent<ForWoodcutter>().rooted)
            {
                PlantRoots();
                other.GetComponent<PlayerController>().blockMoveTime = 2f;
                other.GetComponent<AudioSource>().PlayOneShot(laughSound);


            }
        }

    }

    public void PickUp(Collider other)
    {
        this.transform.SetParent(other.GetComponent<PlayerController>().itemSlot);
        this.transform.position = other.GetComponent<PlayerController>().itemSlot.transform.position;
        other.GetComponent<PlayerController>().carriedItem = this.gameObject;
        other.GetComponent<PlayerController>().pickUpTime = 1f;
        this.GetComponent<ForWoodcutter>().ReservedFor = 999;

    }

    public void PutDown(PlayerController player)
    {
        this.transform.SetParent(GameObject.Find("CarryOn").transform);
        this.transform.position = player.transform.position;
        player.carriedItem = null;
        ForWoodcutter fw = this.GetComponent<ForWoodcutter>();
        fw.ReservedFor = -1;
        //    fw.RootIt();

    }

    public void PlantRoots()
    {
        this.GetComponent<ForWoodcutter>().RootIt();
//        rootsAnimation.Play();
        RootedHandler(true);
        if (this.GetComponent<ForWoodcutter>().GivePoint)
        uiHandler.Score ++;
    }

    public void RootedHandler(bool state)
    {
        if (this.GetComponent<ForWoodcutter>().rooted)
        {
//            rootsOverlay.enabled = state;
            rootsAnimation.Play("Rooting");
        }
    }

    void Update()
    {
//        if (canBeCarried == false)
//        rootsOverlay.enabled = this.GetComponent<ForWoodcutter>().rooted;

    }

}
