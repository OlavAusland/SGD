using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerCombat : NetworkBehaviour
{
    public class UIAbility {
        public GameObject obj;
        public Image image;
        public Image cooldown;

        public UIAbility (GameObject obj){
            this.obj = obj;
            this.image = obj.transform.GetChild(0).GetComponent<Image>();
            this.cooldown = obj.transform.GetChild(1).GetComponent<Image>();
        }

    }

    PlayerManager pm { get { return GetComponent<PlayerManager>(); } }
    public SpriteRenderer sr { get { return transform.GetChild(0).GetComponent<SpriteRenderer>(); } }

    public static PlayerCombat instance;
    
    public Weapon weapon;
    public Transform weaponTransform;
    public List<float> cooldowns = new List<float>();
    public List<UIAbility> uiAbilities = new List<UIAbility>();
    public Transform uiWeapons; //Change Name Does not make sense


    public void Update()
    {
    
        if(isLocalPlayer && weapon)
        {
            Rotate();
            Combat();
        }
    }

    public void SetWeapon()
    {
        if(weapon)
        {
            foreach(Transform child in uiWeapons){Destroy(child.gameObject);}
            cooldowns.Clear();
            uiAbilities.Clear();
            sr.sprite = weapon.icon;
            sr.material = weapon.material;
            for(int i = 0; i < weapon.abilities.Count; i++)
            {
                cooldowns.Add(weapon.abilities[i].ability.cooldown);
                
                GameObject uiAbility = Instantiate(Resources.Load<GameObject>("UI/Ability"), Vector2.zero, Quaternion.identity);
                uiAbilities.Add(new UIAbility(uiAbility));
                uiAbility.transform.SetParent(uiWeapons);
                uiAbility.transform.localScale = new Vector3(1, 1, 1);
                uiAbilities[i].image.sprite = weapon.abilities[i].ability.icon;
            }

        }
        else
        {
            uiAbilities.Clear();
            foreach(Transform child in uiWeapons){Destroy(child.gameObject);}
            cooldowns.Clear();
            sr.sprite = null;
            sr.material = null;
        }
    }

    public void Combat()
    {
        for(int i = 0; i < weapon.abilities.Count; i++){
            if(cooldowns[i] <= 0)
            {
                if(Input.GetKey(weapon.abilities[i].key)){
                    weapon.abilities[i].ability.Activate(this.transform);
                    cooldowns[i] = weapon.abilities[i].ability.cooldown;
                }
            }else{
                cooldowns[i] -= Time.deltaTime;
                uiAbilities[i].cooldown.fillAmount = cooldowns[i] / weapon.abilities[i].ability.cooldown;
            }
        }
    }

    Vector2 Direction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        return direction;
    }

    void Rotate(){
        Vector2 dir = Direction();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.AngleAxis(angle + weapon.rotationOffset * (int)(sr.flipY ? -1 : 1), Vector3.forward);
    }
    
    //Display The Direction Between Player And Mouse
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Direction());
    }
}
