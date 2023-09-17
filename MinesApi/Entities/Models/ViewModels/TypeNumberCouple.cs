namespace Entities.Models.ViewModels
{
    public class TypeNumberCouple
    {
        public string? Type { get; set; }
        public string? Number { get; set; }

        public TypeNumberCouple(string? type, string? number)
        {
            Type = type;
            Number = number;
        }
    }
}
