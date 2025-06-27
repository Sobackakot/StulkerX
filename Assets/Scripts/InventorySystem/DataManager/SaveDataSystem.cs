
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;  

public static class SaveDataSystem  
{ 
    public static async Task SaveDataAsync(PersonsDataList data, string filePath) // save data from PersonDataManager
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using(FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await Task.Run(() => formatter.Serialize(stream,data));
        }
    }
    public static async Task<PersonsDataList> LoadDataAsync(string filePath) // load data from PersonDataManager
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return await Task.Run(() => formatter.Deserialize(stream)) as PersonsDataList;
            } 
        }
        return null;
    }
}
