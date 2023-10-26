namespace BlogApi.Application.Utilities.DTOs.ValidationDTOs
{
    public class ValidationResultDTO
    {
        public ValidationResultDTO()
        {
            Conditions = new List<ValidationConditionDTO>();
        }

        public List<ValidationConditionDTO> Conditions { get; set; }
    }
}
