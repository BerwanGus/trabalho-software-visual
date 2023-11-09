'use client';

import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { SalesList } from '@/app/components/SalesList';

export default function Home() {
  const [ sales, setSales ] = useState<SaleProps[] | undefined>(undefined)
  const [ isLoading, setIsLoading ] = useState<boolean>(true)

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
      <header></header>
      <main className="flex min-h-screen flex-col items-center justify-between p-24">
        {
          isLoading ? <h1>Carregando</h1>
          : sales ? <SalesList sales={sales} />
          : <div>Error</div> 
        }
      </main> 
    </>
  )
}
