import axios from "axios";
import { redirect } from "next/navigation";
import { forwardRef, useEffect, useState } from "react";

export interface AddSaleResumeProps {
  client: ClientProps | undefined;
  seller: SellerProps | undefined;
  event: EventProps | undefined;
  products: {
    data: ProductProps;
    qtd: string;
  }[]
}

interface ProductSaleDTO {
  productID: string;
  productSalesQuantity: number;
}

interface SalePostProps  {
  value: number;
  client_id: string;
  event_id: string;
  seller_id: string;
  sale_Date: Date;
  productSaleDTOs: ProductSaleDTO[];
}

export function AddSaleResume({ client, seller, event, products }: AddSaleResumeProps) {
  const [ isCreateButtonDisabled, setIsCreateButtonDisabled ] = useState<boolean>(true)

  useEffect(() => {
    if (client && seller && event && products.length > 0) setIsCreateButtonDisabled(false)
    else setIsCreateButtonDisabled(true)

    console.log(products)
  }, [ client, seller, event, products ])

  function handleSubmitSale(e: React.FormEvent) {
    // e.preventDefault()
    let finalValue = 0
    let productSaleDTOs: ProductSaleDTO[] = []

    for (const product of products) {
      finalValue += product.data.price
      productSaleDTOs.push({productID: product.data.id, productSalesQuantity: parseInt(product.qtd)})
    }

    const requestBody: SalePostProps = {
      client_id: client!.id,
      event_id: event!.id,
      seller_id: seller!.id,
      value: finalValue,
      productSaleDTOs,
      sale_Date: new Date()
    }

    axios.post("http://localhost:5251/api/Sale", requestBody)
      .then(response => {
        console.log('Venda cadastrada')
        alert('Venda cadastrada')
      })
      .catch(error => {
        console.log(error)
      })
  }
  
  return (
    <div>
      <h2 className="font-extrabold text-accent text-5xl mt-16">RESUMO</h2>

      <h2 className='text-5xl font-extrabold mb-6'>PRODUTOS</h2>
      {
        products.length > 0 &&
        <div className="grid grid-cols-4 gap-10">
          {/* PRODUTOS */}
          {
            products.map((product, index) => (
              <div key={index} className='flex w-full text-lg font-semibold flex-col gap-2 border-b-[1px] border-accent last:border-b-0 pb-6'>
                <div className='flex w-full justify-between'>
                  {/* <div>
                    <a className='text-sm text-accent'>marca</a>
                    <h3 className='leading-none'>{product.data.brand.name}</h3>
                  </div> */}
                  {/* <div>
                    <a className='text-sm text-accent'>nome</a>
                    <h3 className='leading-none'>{product.data.productType.typeName} {product.data.productType.style}</h3>
                  </div> */}
                  <div>
                    <a className='text-sm text-accent'>tamanho</a>
                    <h3 className='leading-none'>{product.data.size}</h3>
                  </div>
                  <div>
                    <a className='text-sm text-accent'>qtd</a>
                    <h3 className='leading-none'>{product.qtd}x</h3>
                  </div>
                </div>

                <div className='flex w-full justify-between'>
                  <div>
                    <a className='text-sm text-accent'>custo</a>
                    <h3 className='leading-none'>R${product.data.cost}</h3>
                  </div>
                  <div>
                    <a className='text-sm text-accent'>preço</a>
                    <h3 className='leading-none'>R${product.data.price}</h3>
                  </div>
                  <div>
                    <a className='text-sm text-accent'>profit</a>
                    <h3 className='leading-none'>R${product.data.price - product.data.cost}</h3>
                  </div>
                </div>
              </div>
            ))
          }
        </div>
      }

      <div className="grid grid-cols-3 gap-32">

        {/* CLIENTE */}
        {
          client &&
            <div>
              <h2 className='text-5xl font-extrabold my-6'>CLIENTE</h2>
              <div className='flex w-full text-lg font-semibold flex-col gap-2 pb-6'>
                <div className='flex w-full justify-between'>
                  <div>
                    <a className='text-sm text-accent'>nome</a>
                    <h3 className='leading-none'>{client.name}</h3>
                  </div>
                  <div>
                    <a className='text-sm text-accent'>cpf</a>
                    <h3 className='leading-none'>{client.cpf}</h3>
                  </div>
                </div>

                <div className='flex w-full justify-between'>
                  <div>
                    <a className='text-sm text-accent'>qtd compras</a>
                    <h3 className='leading-none'>{client.purchases_Quantity}</h3>
                  </div>
                  <div>
                    <a className='text-sm text-accent'>registro</a>
                    <h3 className='leading-none'>{client.register_Date}</h3>
                  </div>
                </div>
              </div>
            </div>
        }

        {/* VENDEDOR */}
        {
          seller &&
          <div>
          <h2 className='text-5xl font-extrabold my-6'>VENDEDOR</h2>
          <div className='flex w-full text-lg font-semibold flex-col gap-2 pb-6'>
            <div className='flex w-full justify-between'>
              <div>
                <a className='text-sm text-accent'>nome</a>
                <h3 className='leading-none'>{seller.name}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>cpf</a>
                <h3 className='leading-none'>{seller.cpf}</h3>
              </div>
            </div>

            <div className='flex w-full justify-between'>
              <div>
                <a className='text-sm text-accent'>qtd vendas</a>
                <h3 className='leading-none'>{seller.sales_Quantity}</h3>
              </div>
            </div>
          </div>
          </div>
        }

        {/* EVENTO */}
        {
          event &&
          <div>
            <h2 className='text-5xl font-extrabold my-6'>EVENTO</h2>
            <div className='flex w-full text-lg font-semibold flex-col gap-2 pb-6'>
              <div className='flex w-full justify-between'>
                <div>
                  <a className='text-sm text-accent'>nome</a>
                  <h3 className='leading-none'>{event.name}</h3>
                </div>
                <div>
                  <a className='text-sm text-accent'>data evento</a>
                  <h3 className='leading-none'></h3>
                </div>
              </div>

              <div className='flex w-full justify-between'>
                <div>
                  <a className='text-sm text-accent'>qtd vendas</a>
                  <h3 className='leading-none'>{event.sales_Quantity}</h3>
                </div>
              </div>
            </div>
          </div>
        }
      </div>

      <button
        type="submit"
        className={`fixed bottom-5 right-5 bg-accent py-4 px-8 text-2xl text-body-white font-extrabold rounded-xl transition-all ${isCreateButtonDisabled ? 'opacity-50' : 'hover:opacity-80'}`}
        disabled={isCreateButtonDisabled}
        onClick={handleSubmitSale}
      >
        CRIAR VENDA
      </button>
    </div>
  )
}