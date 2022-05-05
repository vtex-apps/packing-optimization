namespace PackingOptimization.Data
{
    using PackingOptimization.Models;
    using System.Threading.Tasks;

    public interface IMerchantSettingsRepository
    {
        Task<MerchantSettings> GetMerchantSettings();

        Task<bool> SetMerchantSettings(MerchantSettings merchantSettings);
    }
}
