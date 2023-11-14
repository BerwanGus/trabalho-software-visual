import React, { useState } from 'react';
import Modal from 'react-modal'
import { SaleModal } from './Modal';

interface SaleComponentProps {
  sale: SaleProps
}

export function Sale({sale}: SaleComponentProps) {
  const [ isModalOpen, setIsModalOpen ] = useState<boolean>(false)

  function handleIsModalOpen() {
    console.log(`isModalOpen ${isModalOpen}`)
    setIsModalOpen(!isModalOpen)
  }
  
  return(
    <>
      <div onClick={handleIsModalOpen} className="flex items-center bg-body-white text-body border-b-[1px] border-accent py-4 last:border-b-0 hover:drop-shadow-lg transition-all rounded-xl cursor-pointer">
        {
          sale.products.map(productSale => (
            <h2 className="w-1/4 font-bold" key={productSale.id}>{productSale.productType} <a className="text-accent">{productSale.productQuantity}x</a></h2>
          ))
        }
        <h2 className="w-1/4 font-semibold">{sale.client.name}</h2>
        <h2 className="w-1/4 font-semibold">{sale.seller.name}</h2>
        <h2 className="w-1/4 font-bold">R${sale.value}</h2>
      </div>
      
      {
        isModalOpen ||
        <SaleModal handleIsModalOpen={handleIsModalOpen} sale={sale} />
      }
    </>
  )
}