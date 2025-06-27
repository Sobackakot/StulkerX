using System.IO; 
using UnityEngine;
using Zenject;

public class PersonDataManager  
{    
    private PersonsDataList  dataList;
    private void Start()
    {
        dataList = new PersonsDataList();
    } 
    private void AddDataPerson(PersonData data) // add new person for PersonsDataList from CharacterSwitchSystem
    {
        if (dataList.dataPersons.Contains(data)) return;
        dataList.dataPersons.Add(data);
        Debug.Log("dataList" + dataList.dataPersons.Count);
    }
    private void RemoveDataPerson(PersonData data) //remove person from PersonsDataList from CharacterSwitchSystem...
    {
        if (!dataList.dataPersons.Contains(data)) return;
        dataList.dataPersons.Remove(data);
        Debug.Log("dataList" + dataList.dataPersons.Count);
    }
    private async void SaveData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Data.txt");
        await SaveDataSystem.SaveDataAsync(dataList, filePath); 
    }
    private async void LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Data.txt");
        dataList = await SaveDataSystem.LoadDataAsync(filePath); 
    }
   
    public void SavePoisition(PersonDataScript dataScript,Transform person)
    {
        dataScript?.data.SavePositionPerson(ref person);
    }
    public Vector3 LoadPosition(PersonDataScript dataScripts)
    { 
        Vector3 newPosition = dataScripts.data.LoadPositionPerson();
        return newPosition;
    }
}
