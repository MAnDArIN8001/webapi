namespace api {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile()
        {
            CreateMap<Account, GetAccountDto>();
            CreateMap<AddAccountDto, Account>();
            CreateMap<GetAccountDto, Account>();
        }
    }
}