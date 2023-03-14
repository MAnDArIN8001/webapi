namespace src.Dtos.Group {
    public class GetAccountDto {
        public string id {get; set;}
        
        public string firstName {get; set;} = "default";
        public string secondName {get; set;} = "default";
        public string mail {get; set;} = "default";
    }
}