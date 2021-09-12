namespace Core.Entities
{
    public class RelatedPersonEntity : BaseEntity<int>
    {
        public RelatedPersonTypeEntity RelationType { get; set; }
        public int RelationTypeId { get; set; }
        public PersonEntity Person { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }
}
