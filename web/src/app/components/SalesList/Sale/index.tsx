interface SaleComponentProps {
  sale: SaleProps
}

export function Sale({sale}: SaleComponentProps) {
  return(
    <div className="flex items-center text-body border-b-[1px] border-accent py-4 last:border-b-0">
      {
        sale.products.map(productSale => (
          <h2 className="w-1/4 font-bold" key={productSale.id}>{productSale.productType} <a className="text-accent">{productSale.productQuantity}x</a></h2>
        ))
      }
      <h2 className="w-1/4 font-semibold">{sale.client.name}</h2>
      <h2 className="w-1/4 font-semibold">{sale.seller.name}</h2>
      <h2 className="w-1/4 font-bold">R${sale.value}</h2>
    </div>
  )
}