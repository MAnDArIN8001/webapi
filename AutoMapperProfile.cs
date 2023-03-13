namespace api {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile()
        {
            CreateMap<Student, GetStudentDto>();
            CreateMap<AddStudentDto, Student>();
        }
    }
}