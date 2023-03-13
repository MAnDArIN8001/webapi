namespace api.Models {
    public class Student {
        public int id {get; set;}
        public int age {get; set;}

        public float avrMark {get; set;}

        public string firstName {get; set;} = "default";
        public string secondName {get; set;} = "default";

        public Specialization specialization {get; set;}
    }
}