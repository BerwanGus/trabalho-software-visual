namespace API.Dto;

public class ClientDTO
{
  public string? Name { get; set; }
  public string? Cpf { get; set; }
}

public class EventDTO
{
  public string? Name { get; set; }
  public DateTime? Event_Date { get; set; }
  public float Sales_Quantity { get; set; }
}

public class SellerDTO
{
  public string? Name { get; set; }
  public string? Cpf { get; set; }
}

public class SaleDTO
{
  public float Value { get; set; }
  public DateTime? Sale_Date { get; set; }

  public string? Client_Id { get; set; }
  public string? Seller_Id { get; set; }
  public string? Event_Id { get; set; }

  public ICollection<ProductSaleDTO>? ProductSaleDTOs { get; set; }
}

public class ProductSaleDTO
{
  public required string ProductID { get; set; }
  public required int ProductSalesQuantity { get; set; }
}
public class SellerDTO
{
  public string? Name { get; set; }
  public string? Cpf { get; set; }
}


//Products DTO:
public class ProductTypeDTO
{
  public string? TypeName { get; set; }
  public string? Style { get; set; }


}
public class BrandDTO
{
  public string? Name { get; set; }
}

public class ProductDTO
{
  public string? Size { get; set; }
  public string? Gender { get; set; }
  public string? Style { get; set; }
  public string? Condition { get; set; }
  public float Cost { get; set; }
  public float Price { get; set; }
  public string Color { get; set; }
  public required string Brand_Id { get; set; }
  public required string Type_Id { get; set; }
}