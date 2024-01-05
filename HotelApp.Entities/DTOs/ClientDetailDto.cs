namespace HotelApp.Entities.DTOs
{
    public class ClientDetailDto
    {
        public DateTime AddDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public int CompanyId { get; set; }
    }
}
