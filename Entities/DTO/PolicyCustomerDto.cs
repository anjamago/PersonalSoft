namespace Entities.DTO;

public class PolicyCustomerDto
{
    public string Id { get; set; }
    public string IdCustomer { get; set; }
    public string policyNumber { set; get; }
    public string CreateDate { set; get; }
    public string StartDate { set; get; }
    public string EndDate { set; get; }
    
    public string Plaque { set; get; }
    public string Model { set; get; }
    public bool Inspection { set; get; }
    
    public string Name { set; get; }
    public string Identification { set; get; }
    public string City { set; get; }
    public string Address { set; get; }

}