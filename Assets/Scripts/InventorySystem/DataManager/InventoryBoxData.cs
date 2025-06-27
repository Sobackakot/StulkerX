using System;

[Serializable]
public class InventoryBoxData 
{
    private string id;
    public string ID { get { return id; } private set { id = value; } }

    private bool isInstalled;

    public void SetNewInventoryBoxId() //coll from class CharacterSwitchSystem
    {
        if (!isInstalled) //set unique id
        {
            this.id = Guid.NewGuid().ToString();
            isInstalled = true;
        }
    }
}
