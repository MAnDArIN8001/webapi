using System.Threading.Tasks;

namespace src.Services {
    public interface IAccountServices {
        Task<ServiceResponse<List<GetAccountDto>>> getAccounts();
        Task<ServiceResponse<List<GetAccountDto>>> addAccount(AddAccountDto newAccount);
        Task<ServiceResponse<List<GetAccountDto>>> deleteAccount(string id);

        Task<ServiceResponse<GetAccountDto>> getAccount(string id);

        Task<ServiceResponse<GetAccountDto>> updateAccount(UpdateAccountDto updatedAccount);
    }
    
}