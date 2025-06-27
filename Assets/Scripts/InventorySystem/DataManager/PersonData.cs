using System;
using UnityEngine;

[Serializable]
public class PersonData  
{ 
    private string id; 
    public string ID { get { return id; } private set { id = value; } } // use from class PickUpPerson, PickUpPersonUI
     
    [Range(1, 100)]
    private float currentHP;
    public float CurrentHP { get { return currentHP; } set { currentHP = value; } }

    private float x;
    private float y;
    private float z;

    private bool isInstalled;
    
    public void SetNewPersonId() //coll from class CharacterSwitchSystem
    {
        if (!isInstalled) //set unique id
        {
            this.id = Guid.NewGuid().ToString();
            isInstalled = true; 
        } 
    }   
    public void SavePositionPerson(ref Transform person) // coll from class PersonDataManager
    {
        x = person.position.x;
        y = person.position.y;
        z = person.position.z;
    }
    public Vector3 LoadPositionPerson()// coll from class PersonDataManager
    {
        float _x = x;
        float _y = y;
        float _z = z;
        return new Vector3(_x, _y, _z);
    }
}
