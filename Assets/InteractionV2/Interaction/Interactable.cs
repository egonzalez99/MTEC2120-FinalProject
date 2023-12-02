using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[Header("Interaction Data")]
	public string interactableName="";
	public float interactionDistance = 2;
	[SerializeField] bool isInteractable = true;

	public InteractableNameText interactableNameText;
	public GameObject interactableNameCanvas;

	public virtual void Start()
	{
		interactableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");

		if (interactableNameCanvas == null) return;
		interactableNameText = interactableNameCanvas.GetComponentInChildren<InteractableNameText>();

	}

	public void TargetOn()
	{
		//if (interactableNameText == null) return; 
        interactableNameText.ShowText(this);
        interactableNameText.SetInteractableNamePosition(this);
		//Debug.Log("ShowText ");
	}

	public void TargetOff()
	{
		//if (interactableNameText == null) return;

		interactableNameText.HideText();
		//Debug.Log("HideText ");

	}

	public void Interact(GameObject player)
	{
		if (isInteractable) Interaction(player);
	}


	public void Interact()
	{
		if (isInteractable) Interaction();
	}


	protected virtual void Interaction(GameObject player)
	{
		Debug.Log("interact with: " + this.name);
		Debug.Log("Player : " + player.name);

	}

	protected virtual void Interaction()
	{
		Debug.Log("interact with: " + this.name);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position,interactionDistance);
	}
	private void OnDestroy()
	{
		TargetOff();
    }
}
