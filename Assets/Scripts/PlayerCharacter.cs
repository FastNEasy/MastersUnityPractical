using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character
{
    CharacterController _characterController;
    [SerializeField] Slider _healthSlider;
    [SerializeField] AudioSource _audioGettingHitSrc;
    [SerializeField] AudioSource _ambienceSound;

    [SerializeField] AudioSource winSrc;
    [SerializeField] AudioSource loseSrc;
    [SerializeField] GameObject _youWonScreen;
    [SerializeField] GameObject _youLostScreen;

    private bool winScreenShown = false;

    protected override void Start(){
        base.Start();
        _characterController = GetComponent<CharacterController>();
    }

    void Update(){
         if(GameManager.instance.enemyCount == 0){
            if(!winScreenShown){
                displayWinScreen();
                winScreenShown = true;
            }
        }
    }
      
    protected override void Die(){
        base.Die();
        Debug.Log("You died");
        displayLoseScreen();
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
             _audioGettingHitSrc.Stop();
             _audioGettingHitSrc.Play();
            Debug.Log("I got hit by skelly for " + weapon.damage);
            AddDamage(weapon.damage);
       }
    }

    void displayWinScreen(){
        Debug.Log("You won");
        _youWonScreen.SetActive(true);
        _ambienceSound.Stop();
        winSrc.Play();
        GameManager.instance.GameWin();
    }

    void displayLoseScreen(){
        Debug.Log("You lost");
        _youLostScreen.SetActive(true);
        _ambienceSound.Stop();
        loseSrc.Play();
        GameManager.instance.GameLose();
    }
}
