namespace api.Dtos.Group {
    public class AddStudentDto { 
        public int age {get; set;}

        public float avrMark {get; set;}

        public string firstName {get; set;} = "default";
        public string secondName {get; set;} = "default";
    }
}