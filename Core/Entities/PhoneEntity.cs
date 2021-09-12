namespace Core.Entities
{
    public class PhoneEntity : BaseEntity<int>
    {
        public PhoneTypeEntity Type { get; set; }
        public int TypeId { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }
    }
}
