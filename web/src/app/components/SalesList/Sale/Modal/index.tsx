import { X } from 'react-feather'

interface ModalComponentProps {
  sale: SaleProps,
  handleIsModalOpen: () => void
}

export function SaleModal({sale, handleIsModalOpen}: ModalComponentProps) {
  return (
    <div id='modal' className='absolute flex flex-col bg-body-white text-left w-1/4 border-accent border-l-2 top-0 right-0 z-50 drop-shadow-lg p-8'>
      {/* HEADER */}
      <div className='mb-10'>
        <X
          onClick={handleIsModalOpen}
          color='#50947D'
          className='absolute top-8 right-8 cursor-pointer hover:opacity-80 transition-all z-20'
        />
        <h4 className='text-left text-accent opacity-25 font-bold'>
          #{sale.id}
        </h4>
      </div>

      {/* PRODUTOS */}
      <h2 className='text-5xl font-extrabold mb-6'>PRODUTOS</h2>
      {
        sale.products.map(product => (
          <div key={product.id} className='flex w-full text-lg font-semibold flex-col gap-2 border-b-[1px] border-accent last:border-b-0 pb-6'>
            <div className='flex w-full justify-between'>
              <div>
                <a className='text-sm text-accent'>marca</a>
                <h3 className='leading-none'>{product.brandName}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>nome</a>
                <h3 className='leading-none'>{product.productType}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>tamanho</a>
                <h3 className='leading-none'>{product.size}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>qtd</a>
                <h3 className='leading-none'>{product.productQuantity}x</h3>
              </div>
            </div>

            <div className='flex w-full justify-between'>
              <div>
                <a className='text-sm text-accent'>custo</a>
                <h3 className='leading-none'>R${product.cost}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>preço</a>
                <h3 className='leading-none'>R${product.price}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>profit</a>
                <h3 className='leading-none'>R${product.productProfit}</h3>
              </div>
            </div>
          </div>
        ))
      }

      {/* CLIENTE */}
      {
        sale.client ?
          <>
            <h2 className='text-5xl font-extrabold my-6'>CLIENTE</h2>
            <div className='flex w-full text-lg font-semibold flex-col gap-2 pb-6'>
              <div className='flex w-full justify-between'>
                <div>
                  <a className='text-sm text-accent'>nome</a>
                  <h3 className='leading-none'>{sale.client.name}</h3>
                </div>
                <div>
                  <a className='text-sm text-accent'>cpf</a>
                  <h3 className='leading-none'>{sale.client.cpf}</h3>
                </div>
              </div>

              <div className='flex w-full justify-between'>
                <div>
                  <a className='text-sm text-accent'>qtd compras</a>
                  <h3 className='leading-none'>{sale.client.purchases_Quantity}</h3>
                </div>
                <div>
                  <a className='text-sm text-accent'>registro</a>
                  <h3 className='leading-none'>{sale.client.register_Date}</h3>
                </div>
              </div>
            </div>
          </>
        : <p>Compra com cliente nulo</p>
      }

      {/* VENDEDOR */}
      {
        sale.seller ?
        <>
        <h2 className='text-5xl font-extrabold my-6'>VENDEDOR</h2>
        <div className='flex w-full text-lg font-semibold flex-col gap-2 pb-6'>
          <div className='flex w-full justify-between'>
            <div>
              <a className='text-sm text-accent'>nome</a>
              <h3 className='leading-none'>{sale.seller.name}</h3>
            </div>
            <div>
              <a className='text-sm text-accent'>cpf</a>
              <h3 className='leading-none'>{sale.seller.cpf}</h3>
            </div>
          </div>

          <div className='flex w-full justify-between'>
            <div>
              <a className='text-sm text-accent'>qtd vendas</a>
              <h3 className='leading-none'>{sale.seller.sales_Quantity}</h3>
            </div>
          </div>
        </div>
      </>
        : <p>Compra com vendedor nulo</p>
      }

      {/* EVENTO */}
      {
        sale.event ?
        <>
          <h2 className='text-5xl font-extrabold my-6'>EVENTO</h2>
          <div className='flex w-full text-lg font-semibold flex-col gap-2 pb-6'>
            <div className='flex w-full justify-between'>
              <div>
                <a className='text-sm text-accent'>nome</a>
                <h3 className='leading-none'>{sale.event.name}</h3>
              </div>
              <div>
                <a className='text-sm text-accent'>data evento</a>
                <h3 className='leading-none'></h3>
              </div>
            </div>

            <div className='flex w-full justify-between'>
              <div>
                <a className='text-sm text-accent'>qtd vendas</a>
                <h3 className='leading-none'>{sale.event.sales_Quantity}</h3>
              </div>
            </div>
          </div>
        </>
        : <p>Compra com evento nulo</p>
      }

      <h2 className='text-5xl font-extrabold my-6'>OVERALL</h2>
    </div>
  )
}