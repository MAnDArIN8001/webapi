using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace src.Services.StudentServec {
    public class AccountService : IAccountServices {
        private readonly IMapper _mapper;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Account> _mongoCollection;

        public AccountService(IMapper mapper) {
            _mapper = mapper;  

            _mongoClient = new MongoClient("mongodb+srv://Admin:123321a@cluster0.l704ugs.mongodb.net/?retryWrites=true&w=majority"); 
            _database = _mongoClient.GetDatabase("accountBase");
            _mongoCollection = _database.GetCollection<Account>("accounts");
        }
        
        public async Task<ServiceResponse<List<GetAccountDto>>> addAccount(AddAccountDto newAccount) {
            var response = new ServiceResponse<List<GetAccountDto>>(); 
            var account = _mapper.Map<Account>(newAccount);

            account.id = ObjectId.GenerateNewId();

            try {
                await _mongoCollection.InsertOneAsync(account);

                response.Data = _mapper.Map<List<GetAccountDto>>(_mongoCollection.Find(FilterDefinition<Account>.Empty).ToList());
                response.Success = true;
            } catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetAccountDto>>> getAccounts() {
            var response = new ServiceResponse<List<GetAccountDto>>();

            try {
                var accounts = _mongoCollection.Find(FilterDefinition<Account>.Empty).ToList();

                response.Data = accounts.Select(account => _mapper.Map<GetAccountDto>(account)).ToList();
                response.Success = true;
            } catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetAccountDto>> getAccount(string id) {
            var response = new ServiceResponse<GetAccountDto>();

            try {
                var account = _mongoCollection.Find(account => account.id == ObjectId.Parse(id)).FirstOrDefault();

                if(account is null) {
                    throw new Exception($"Cant change user with id '{id}'");
                } 

                response.Data = _mapper.Map<GetAccountDto>(account);
                response.Success = true;
            } catch(Exception ex) {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetAccountDto>> updateAccount(UpdateAccountDto updateAccount) {
            var response = new ServiceResponse<GetAccountDto>();

            var updateOptions = new FindOneAndUpdateOptions<Account> {ReturnDocument=ReturnDocument.After};
            var updateDefenitions = Builders<Account>.Update.Set(account => account.mail, updateAccount.mail).
                Set(account => account.firstName, updateAccount.firstName).
                Set(account => account.secondName, updateAccount.secondName).
                Set(account => account.password, updateAccount.password);

            try {
                var account = _mongoCollection.Find(account => account.id == ObjectId.Parse(updateAccount.id)).FirstOrDefault();

                if(account is null) {
                    throw new Exception($"Account with id '{updateAccount.id}' not found");
                }

                _mongoCollection.FindOneAndUpdate<Account>(account => account.id == ObjectId.Parse(updateAccount.id), updateDefenitions, updateOptions); 

                response.Success = true;
            } catch(Exception ex) {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetAccountDto>>> deleteAccount(string id) {
            var response = new ServiceResponse<List<GetAccountDto>>();

            try {
                var account = await _mongoCollection.FindAsync(account => account.id == ObjectId.Parse(id));

                if(account is null) {
                    throw new Exception($"account with id '{id}' not found");
                }

                _mongoCollection.DeleteOne(deleted => deleted.id == ObjectId.Parse(id));

                response.Data = _mongoCollection.Find(FilterDefinition<Account>.Empty).ToList().Select(account => _mapper.Map<GetAccountDto>(account)).ToList();
                response.Success = true;
            } catch(Exception ex) {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}