using DemeterMonitor.Data;

namespace DemeterMonitor.Interface
{
    public interface IPlantInformation
    {

        public Task<PlantData> GetCurrentData();

    }
}
