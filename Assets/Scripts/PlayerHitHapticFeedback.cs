using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHitHapticFeedback : MonoBehaviour
{

    [Range(0, 1)]
    public float intenisty = 0.5f;
    public float duration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
    }

    // Update is called once per frame
    private void TriggerHaptic(XRBaseController controller){
        if(intenisty > 0){
            controller.SendHapticImpulse(intenisty, duration);
        }
    }

    private void OnTriggerEnter(Collider other){
        Weapon weapon = other.GetComponentInParent<Weapon>();
        if(weapon != null && weapon.isPlayerWeapon){
            // TriggerHaptic();
       }
    }
}
