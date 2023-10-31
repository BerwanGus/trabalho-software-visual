'use client';

import React, { useEffect, useState } from 'react';
import axios from 'axios';

interface ClientProps {
  id: string,
  name: string,
  cpf: string,
  register_Date: string,
  purchases_Quantity: number,
  purchases: []
}


export default function Home() {
  const [ clients, setClients ] = useState<ClientProps[] | undefined>(undefined)

  function getClients() {
    axios.get("http://localhost:5251/api/Client")
      .then(res => {
        console.log(res.data)
        setClients(res.data)
      })
      .catch(err => console.error(err))
  }

  useEffect(() => {
    getClients()
  }, [])
  
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <h1>{clients ? clients[0].name : 'Bosta'}</h1>
      <h1>{clients ? clients[0].cpf : 'Bosta'}</h1>
      <h1>{clients ? clients[0].id : 'Bosta'}</h1>
    </main> 
  )
}
