namespace GayaAPIProject.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Implementation { get; set; }
        public bool IsActive { get; set; }
    }
}
