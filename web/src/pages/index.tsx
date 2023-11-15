
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { SalesList } from '@/app/components/SalesList';
import { MainHeader } from '@/app/components/Header';
import { AddSalePopup } from '@/app/components/AddSale';
import { useApi } from '@/app/context/api.context';

export default function Home() {
  const { sales, isLoading } = useApi()

  const [ table, setTable ] = useState<number>(0)

  const links = [
    {
      name: 'Vendas',
      handle: () => handleTableState(0)
    },
    {
      name: 'Produtos',
      handle: () => handleTableState(1)
    },
    {
      name: 'Marcas',
      handle: () => handleTableState(2)
    },
    {
      name: 'Clientes',
      handle: () => handleTableState(3)
    },
    {
      name: 'Vendedores',
      handle: () => handleTableState(4)
    },
    {
      name: 'Eventos',
      handle: () => handleTableState(5)
    },
  ]

  function handleTableState(filter: number) {
    console.log(`Mudando tabela de filtro ${table} para ${filter}`)
    setTable(filter)
  }

  function handleTable() {
    switch (table) {
      case 0:{
        return <SalesList sales={sales!}/>
        

        break;
      }
      case 1:{
        return <SalesList sales={sales!}/>
        
        break;
      }
      case 2:{
        return <SalesList sales={sales!}/>
        
        break;
      }
      case 3:{
        return <SalesList sales={sales!}/>
        
        break;
      }
      case 4:{
        return <SalesList sales={sales!}/>
        
        break;
      }
      case 5:{
        return <SalesList sales={sales!}/>
        
        break;
      }
    }
  }
  
  return (
    <>
      <MainHeader functions={links}/>
      <main className="flex flex-col items-center justify-between p-24 h-full">
        {
          isLoading ? <h1>Carregando</h1>
          : sales.length > 0 ? handleTable()
          : <div>Error</div>
        }
        <AddSalePopup />
      </main> 
    </>
  )
}
