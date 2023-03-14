namespace api.Services {
    public interface IGroupService {
        Task<ServiceResponse<List<GetStudentDto>>> getGroup();
        Task<ServiceResponse<List<GetStudentDto>>> addStudent(AddStudentDto newStudent);
        Task<ServiceResponse<List<GetStudentDto>>> deleteStudent(string id);

        Task<ServiceResponse<GetStudentDto>> GetStudent(string id);

        Task<ServiceResponse<GetStudentDto>> updateStudent(UpdateStudentDto updatedStudent);
    }
    
}