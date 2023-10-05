namespace API.Dto;

public class ClientDTO {
  public string? Name {get; set; }
  public string? Cpf {get; set; }
}

public class EventDTO {
  public string? Name {get; set; }
  public DateTime? Event_Date { get; set; }
  public float Sales_Quantity { get; set; }
}

public class SaleDTO {
  public float Value { get; set; }
  public DateTime? Sale_Date { get; set; }

  public string? Client_Id { get; set; }
  public string? Seller_Id { get; set; }
  public string? Event_Id { get; set; }
}

public class SellerDTO {
  public string? Name {get; set; }
  public string? Cpf {get; set; }
}
