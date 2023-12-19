using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character
{
    CharacterController _characterController;
    [SerializeField] Slider _healthSlider;
    // [SerializeField] Slider _healthSlider;
    protected override void Start(){
        base.Start();
        _characterController = GetComponent<CharacterController>();
    }
      
    // public override void AddDamage(float damage){
    //     base.AddDamage(damage);
    //     if(isDead){

    //     }
    // }
    void Update(){
       
        if(GameManager.instance.enemyCount == 0){
            //if enemies are defeated set up next level
        }
    }
    
    protected override void Die(){
        base.Die();
        Debug.Log("You died");
        // _characterController.enabled = false;

    }
   
    protected override void UpdateHealth()
    {
        base.UpdateHealth();
        _healthSlider.value = HealthPercent;
    }

    void OnTriggerEnter(Collider other){
        Weapon weapon = other.GetComponentInParent<Weapon>();
         if(weapon != null && !weapon.isPlayerWeapon){
            Debug.Log("I got hit by skelly for " + weapon.damage);
            AddDamage(weapon.damage);
       }
    }
}
