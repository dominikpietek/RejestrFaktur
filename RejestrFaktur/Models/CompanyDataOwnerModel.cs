namespace RejestrFaktur.Models
{
    public class CompanyDataOwnerModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }
        public string NIPNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
