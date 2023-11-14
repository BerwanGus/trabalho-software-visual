'use client';

import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { SalesList } from '@/app/components/SalesList';
import { MainHeader } from '@/app/components/Header';

export default function Home() {
  const [ sales, setSales ] = useState<SaleProps[] | undefined>(undefined)
  const [ table, setTable ] = useState<number>(0)
  const [ isLoading, setIsLoading ] = useState<boolean>(true)

  const links = [
    {
      name: 'Sales',
      handle: () => handleTableState(0)
    },
    {
      name: 'Products',
      handle: () => handleTableState(1)
    },
    {
      name: 'Brands',
      handle: () => handleTableState(2)
    },
    {
      name: 'Clients',
      handle: () => handleTableState(3)
    },
    {
      name: 'Sellers',
      handle: () => handleTableState(4)
    },
    {
      name: 'Events',
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

  function getProducts() {
    axios.get("http://localhost:5251/api/Sale")
      .then(res => {
        console.log(res.data)
        setSales(res.data)
        setIsLoading(false)
      })
      .catch(err => {
        setIsLoading(false)
        console.log(err)
      })
  }
  
  useEffect(() => {
    getProducts()
  }, [])
  
  return (
    <>
      <MainHeader functions={links}/>
      <main className="flex flex-col items-center justify-between p-24 h-full">
        {
          isLoading ? <h1>Carregando</h1>
          : sales ? handleTable()
          : <div>Error</div> 
        }
      </main> 
    </>
  )
}
