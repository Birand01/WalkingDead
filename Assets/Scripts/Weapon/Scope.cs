using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public delegate void OnScopeAnimHandler(bool state);
    public static event OnScopeAnimHandler OnScopeEvent;

    [SerializeField] GameObject scopeOverlay;
    private bool isScope;

    private void Update()
    {
        ScopeEventHandler(isScope);
        SetScopeOverlayVisibility();
    }

    private void ScopeEventHandler(bool state)
    {
       
        if (Input.GetMouseButtonDown(1))
        {
            isScope = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isScope = false;
        }
        OnScopeEvent?.Invoke(isScope);
    }
    
    private void SetScopeOverlayVisibility()
    {
        if (isScope)
        {
            StartCoroutine(OnScoped());
        }
        else
        {
            StartCoroutine(OnUnScoped());
        }
            
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.25f);
        Camera.main.fieldOfView = 20;
        scopeOverlay.SetActive(true);
        Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << 11);

    }
    IEnumerator  OnUnScoped()
    {
        yield return new WaitForSeconds(.25f);
        Camera.main.fieldOfView = 60;
        scopeOverlay.SetActive(false);
        Camera.main.cullingMask = Camera.main.cullingMask | (1 << 11);
       
    }
}
