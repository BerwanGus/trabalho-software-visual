import React, { useEffect, useState } from 'react';
import { SaleModal } from './Modal';

interface SaleComponentProps {
  sale: SaleProps
}

export function Sale({sale}: SaleComponentProps) {
  const [ isModalOpen, setIsModalOpen ] = useState<boolean>(false)

  function handleOpenModal() {
    setIsModalOpen(true)
  }

  function handleCloseModal() {
    setIsModalOpen(false)
  }
  
  return(
    <>
      {
        isModalOpen &&
        <SaleModal handleIsModalOpen={handleCloseModal} sale={sale} />
      }
      <div
        onClick={handleOpenModal}
        className={`flex items-center bg-body-white text-body border-b-[1px] border-accent py-4 last:border-b-0 hover:drop-shadow-lg transition-all rounded-xl cursor-pointer`}
      >
        {
          sale.products.map(productSale => (
            <h2 className="w-1/4 font-bold" key={productSale.id}>{productSale.productType} <a className="text-accent">{productSale.productQuantity}x</a></h2>
          ))
        }
        <h2 className="w-1/4 font-semibold">{sale.client.name}</h2>
        <h2 className="w-1/4 font-semibold">{sale.seller.name}</h2>
        <h2 className="w-1/4 font-bold">R${sale.value}</h2>
      </div>
    </>
  )
}