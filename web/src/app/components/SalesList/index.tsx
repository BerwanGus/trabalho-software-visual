import { Sale } from "./Sale";

interface SalesListProps{
  sales: SaleProps[]
}

export function SalesList({sales}: SalesListProps) {
  return (
    <div className="flex flex-col bg-white rounded-xl border-accent border-2 w-full px-4 py-8 text-center shadow-lg">
      <div className="flex font-bold text-accent lowercase">
        <h2 className="w-1/4">Produtos</h2>
        <h2 className="w-1/4">Cliente</h2>
        <h2 className="w-1/4">Vendedor</h2>
        <h2 className="w-1/4">Valor</h2>
      </div>
      {
        sales.map(sale =>
          <Sale sale={sale} key={sale.id}/>
        )
      }
    </div>  
  )
}