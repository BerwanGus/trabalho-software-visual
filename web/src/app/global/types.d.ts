interface ProductProps {
  id:	string,
  size:	string,
  gender:	string,
  condition:	string,
  cost:	number,
  price:	number,
  color:	string,
  brand_Id:	string,
  type_Id:	string,
  brand:	BrandProps,
  productType:	ProductTypeProps,
}

interface BrandProps {
  id: string,
  name: string,
}

interface EventProps {
  id:	string,
  name:	string,
  event_Date:	Date,
  sales_Quantity:	number,
}

interface ProductTypeProps {
  id:	string,
  typeName:	string,
  style:	string,
}

interface SaleProps {
  id:	string,
  sale_Date:	Date,
  value:	number,
  client_Id:	string,
  seller_Id:	string,
  event_Id:	string,
  client:	ClientProps,
  seller:	SellerProps,
  event:	EventProps,
  products:	{
    id: string,
    brandName: string,
    cost: number,
    price: number,
    productProfit: number,
    productQuantity: number,
    productType: string,
    size: string
  }[],
}

interface SellerProps {
  id:	string,
  name:	string,
  cpf:	string,
  sales_Quantity:	number,
}

interface ClientProps {
  id: string,
  name: string,
  cpf: string,
  register_Date: string,
  purchases_Quantity: number,
}

interface ProductSaleProps {
  id:	string,
  product: ProductProps,
  productQuantity: number,
  sale:	SaleProps,
}