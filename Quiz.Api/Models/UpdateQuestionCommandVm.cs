namespace Quiz.Api.Models
{
    public class UpdateQuestionCommandVm
    {
        public int QuestionID { get; set; }
        public int QuizID { get; set; }
        public string question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
        public IFormFile Picture { get; set; }
        public float Score { get; set; }
    }
}
