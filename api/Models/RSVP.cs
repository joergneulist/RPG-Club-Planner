namespace api.Models
{
    public class RSVP
    {
        public int Id { get; set; }
        // DateTimeOffset stores the UTC time + the offset, preserving the 'exact' moment
        public DateTimeOffset SignedUpAt { get; set; }
    }
}
