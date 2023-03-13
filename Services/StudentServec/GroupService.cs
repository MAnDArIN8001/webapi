namespace api.Services.StudentServec {
    public class GroupService : IGroupService {
        private static List<Student> _group = new List<Student> {
            new Student() {id = 0, firstName = "Artem", secondName = "Stepanenko", specialization = Specialization.ISIT, avrMark = 8.3f, age = 17},
            new Student() {id = 1, firstName = "Nikita", secondName = "Tihonenko", specialization = Specialization.ISIT, avrMark = 8.8f, age = 18},
        };

        private readonly IMapper _mapper;

        public GroupService(IMapper mapper) {
            _mapper = mapper;   
        }
        
        public async Task<ServiceResponse<List<GetStudentDto>>> addStudent(AddStudentDto newStudent) {
            var response = new ServiceResponse<List<GetStudentDto>>(); 
            var student = _mapper.Map<Student>(newStudent);

            student.id = _group.Max(student => student.id) + 1;

            try {
                _group.Add(student);

                response.Data = _mapper.Map<List<GetStudentDto>>(_group);
                response.Success = true;
            } catch {
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> getGroup() {
            var response = new ServiceResponse<List<GetStudentDto>>();

            response.Data = _group.Select(student => _mapper.Map<GetStudentDto>(student)).ToList();
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudent(int id) {
            var response = new ServiceResponse<GetStudentDto>();

            response.Data = _mapper.Map<GetStudentDto>(_group.FirstOrDefault(student => student.id == id));
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> updateStudent(UpdateStudentDto updatedStudent) {
            var response = new ServiceResponse<GetStudentDto>();

            try {
                var student = _group.FirstOrDefault(student => student.id == updatedStudent.id);

                if(student is null) {
                    throw new Exception($"Student with id '{updatedStudent.id}' not found");
                }

                student.age = updatedStudent.age;
                student.avrMark = updatedStudent.avrMark;
                student.firstName = updatedStudent.firstName;
                student.secondName = updatedStudent.secondName;
                student.specialization = updatedStudent.specialization;

                response.Data = _mapper.Map<GetStudentDto>(student);
                response.Success = true;
            } catch(Exception ex) {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> deleteStudent(int id) {
            var response = new ServiceResponse<List<GetStudentDto>>();

            try {
                var student = _group.FirstOrDefault(student => student.id == id);

                if(student is null) {
                    throw new Exception($"Student with id '{id}' not found");
                }

                _group.Remove(student);

                response.Data = _group.Select(student => _mapper.Map<GetStudentDto>(student)).ToList();
                response.Success = true;
            } catch(Exception ex) {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}