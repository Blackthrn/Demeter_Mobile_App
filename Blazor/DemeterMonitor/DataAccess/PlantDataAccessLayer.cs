using DemeterMonitor.Data;
using DemeterMonitor.Interface;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace DemeterMonitor.DataAccess
{
    public class PlantDataAccessLayer : IPlantInformation
    {

        string projectId;

        FirestoreDb firestoreDataBase;

        public PlantDataAccessLayer() 
        {

            string databaseFilePath = "\\Resources\\private_key.json";

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_KEY", databaseFilePath); //Might need to change google application credidentials

            projectId = "demeterdb-100d9";
            firestoreDataBase = FirestoreDb.Create(projectId);

        }

        public async Task<PlantData> GetCurrentData()
        {
            
            try
            {

                Query plantQuery = firestoreDataBase.Collection("plants");
                QuerySnapshot plantQuerySnapshot = await plantQuery.GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapshot in plantQuerySnapshot.Documents)
                {

                    if (documentSnapshot.Exists)
                    {

                        Dictionary<string, object> dictionary = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(dictionary);
                        PlantData currentPlantData = JsonConvert.DeserializeObject<PlantData>(json);
                        currentPlantData._currentMoistureLevels = Convert.ToDouble(documentSnapshot.Id);

                    }

                }

            }
            catch (Exception ex)
            {
               
            }

        }
    }
}
