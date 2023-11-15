import axios from "axios"
import { createContext, useContext, useEffect, useMemo, useState } from "react"

interface ApiContextProps {
  // tipagem
  clients: ClientProps[],
  sellers: SellerProps[],
  events: EventProps[],
  products: ProductProps[],
  sales: SaleProps[],
  isLoading: boolean
}

const ApiContext = createContext<ApiContextProps>({
  clients: [],
  sellers: [],
  events: [],
  products: [],
  sales: [],
  isLoading: true
})

export function ApiProvider(props: any) {
  
  
  const [ clients, setClients ] = useState<ClientProps[]>([])  
  const [ sellers, setSellers ] = useState<SellerProps[]>([])
  const [ events, setEvents ] = useState<EventProps[]>([])
  const [ products, setProducts ] = useState<ProductProps[]>([])
  const [ sales, setSales ] = useState<SaleProps[]>([])
  const [ isLoading, setIsLoading ] = useState<boolean>(true)

  async function searchAllInfo() {
    // ----- SALE
    await axios.get("http://localhost:5251/api/Sale")
      .then(res => {
        // console.log(res.data)
        setSales(res.data)
        setIsLoading(false)
      })
      .catch(err => {
        setIsLoading(false)
        console.log(err)
      })

    // ----- PRODUCTS
    await axios.get("http://localhost:5251/api/Product")
      .then(res => {
        // console.log(res.data)
        setProducts(res.data)
        setIsLoading(false)
      })
      .catch(err => {
        setIsLoading(false)
        console.log(err)
      })

    // ----- CLIENTS
    await axios.get("http://localhost:5251/api/Client")
      .then(res => {
        // console.log(res.data)
        setClients(res.data)
        setIsLoading(false)
      })
      .catch(err => {
        setIsLoading(false)
        console.log(err)
      })

    // ----- SELLERS
    await axios.get("http://localhost:5251/api/Seller")
      .then(res => {
        // console.log(res.data)
        setSellers(res.data)
        setIsLoading(false)
      })
      .catch(err => {
        setIsLoading(false)
        console.log(err)
      })

    // ----- EVENTS
    await axios.get("http://localhost:5251/api/Event")
      .then(res => {
        // console.log(res.data)
        setEvents(res.data)
        setIsLoading(false)
      })
      .catch(err => {
        setIsLoading(false)
        console.log(err)
      })
  }

  useEffect(() => {
    searchAllInfo()
  }, [])

  return (
    <ApiContext.Provider 
      value={{
        clients: useMemo(() => (clients), [clients]),
        sellers: useMemo(() => (sellers), [sellers]),
        events: useMemo(() => (events), [events]),
        products: useMemo(() => (products), [products]),
        sales: useMemo(() => (sales), [sales]),
        isLoading: useMemo(() => (isLoading), [isLoading]),
      }}
      {...props}
    />
  )
}

// criação de um hook com o context
export const useApi = () => useContext(ApiContext)