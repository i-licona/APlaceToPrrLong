namespace APlaceToPrrLong.DTOs.Pet;

public class AddPetDTO
{
    public DateTime AdoptionDate { get; set; }
    public string Description { get; set; }
    public string Nombre { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public DateTime PostDate { get; set; }
    public int UserId { get; set;}
}