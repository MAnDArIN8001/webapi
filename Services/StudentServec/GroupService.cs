using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Services.StudentServec {
    public class GroupService : IGroupService {
        private readonly IMapper _mapper;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Student> _mongoCollection;

        public GroupService(IMapper mapper) {
            _mapper = mapper;  

            _mongoClient = new MongoClient("mongodb+srv://Admin:123321a@cluster0.l704ugs.mongodb.net/?retryWrites=true&w=majority"); 
            _database = _mongoClient.GetDatabase("group");
            _mongoCollection = _database.GetCollection<Student>("students");
        }
        
        public async Task<ServiceResponse<List<GetStudentDto>>> addStudent(AddStudentDto newStudent) {
            var response = new ServiceResponse<List<GetStudentDto>>(); 
            var student = _mapper.Map<Student>(newStudent);

            student.id = ObjectId.GenerateNewId();

            try {
                await _mongoCollection.InsertOneAsync(student);

                response.Data = _mapper.Map<List<GetStudentDto>>(_mongoCollection.Find(FilterDefinition<Student>.Empty).ToList());
                response.Success = true;
            } catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> getGroup() {
            var response = new ServiceResponse<List<GetStudentDto>>();

            try {
                var group = _mongoCollection.Find(FilterDefinition<Student>.Empty).ToList();

                response.Data = group.Select(student => _mapper.Map<GetStudentDto>(student)).ToList();
                response.Success = true;
            } catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudent(string id) {
            var response = new ServiceResponse<GetStudentDto>();

            try {
                var student = await _mongoCollection.FindAsync(student => student.id == ObjectId.Parse(id));

                if(student is null) {
                    throw new Exception($"Cant change user with id '{id}'");
                } 

                response.Data = _mapper.Map<GetStudentDto>(student);
                response.Success = true;
            } catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> updateStudent(UpdateStudentDto updatedStudent) {
            var response = new ServiceResponse<GetStudentDto>();

            var updateOptions = new FindOneAndUpdateOptions<Student> {ReturnDocument=ReturnDocument.After};
            var updateDefenitions = Builders<Student>.Update.Set(student => student.age, updatedStudent.age).
                Set(student => student.firstName, updatedStudent.firstName).
                Set(student => student.secondName, updatedStudent.secondName).
                Set(student => student.avrMark, updatedStudent.avrMark);

            try {
                var student = _mongoCollection.Find(student => student.id == ObjectId.Parse(updatedStudent.id)).FirstOrDefault();

                if(student is null) {
                    throw new Exception($"Student with id '{updatedStudent.id}' not found");
                }

                _mongoCollection.FindOneAndUpdate<Student>(student => student.id == ObjectId.Parse(updatedStudent.id), updateDefenitions, updateOptions); 

                response.Success = true;
            } catch(Exception ex) {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> deleteStudent(string id) {
            var response = new ServiceResponse<List<GetStudentDto>>();

            try {
                var student = await _mongoCollection.FindAsync(student => student.id == ObjectId.Parse(id));

                if(student is null) {
                    throw new Exception($"Student with id '{id}' not found");
                }

                _mongoCollection.DeleteOne(deleted => deleted.id == ObjectId.Parse(id));

                response.Data = _mongoCollection.Find(FilterDefinition<Student>.Empty).ToList().Select(student => _mapper.Map<GetStudentDto>(student)).ToList();
                response.Success = true;
            } catch(Exception ex) {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}