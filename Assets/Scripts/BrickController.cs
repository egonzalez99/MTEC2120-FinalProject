using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class BrickController : MonoBehaviour
{

    public delegate void OnBrickRestore();
    public static event OnBrickRestore onBrickRestore;

    private bool isRestored = false; 


    void OnRestore(InputValue value)
    {
        if(value.isPressed && !isRestored)
        {
            onBrickRestore.Invoke();

            isRestored = true;
            StartCoroutine(AllowRestore());


        }
    }


    IEnumerator AllowRestore()
    {
        yield return new WaitForSeconds(2.0f);
        isRestored = false;
    }
}
