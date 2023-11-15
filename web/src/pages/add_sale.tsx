import { AddSaleResume, AddSaleResumeProps } from "@/app/components/AddSaleResume";
import { MainHeader } from "@/app/components/Header";
import { useApi } from "@/app/context/api.context";
import React, { useEffect, useState } from 'react';
import { Minus, Plus } from "react-feather";

interface ProductInfo {
  product_id: string;
  qtd: string;
}

interface SaleFormProps {
  client_id: string;
  event_id: string;
  seller_id: string;
  products: ProductInfo[];
}

interface SearchProductProps {
  product: ProductInfo,
  index: number
}

export default function AddSale() { 
  // --------------------> CHATGPT -------------------
  // Chatgpt fez a lógica por traz de ir adicionando produtos dinamicamente...

  const [ numProducts, setNumProducts ] = useState<number>(1);
  const [ productInfo, setProductInfo ] = useState<ProductInfo[]>(Array.from({ length: 1 }, () => ({ product_id: '', qtd: '1' })));
  
  function handleNumProductsChange(one: number) {
    
    setNumProducts(numProducts + one);
    setProductInfo(Array.from({ length: numProducts + one }, (_, index) => productInfo[index] || { product_id: '', qtd: '1' }));

    if (one < 0) {
      setSaleFormInfo({...saleFormInfo, products: saleFormInfo["products"].slice(0, saleFormInfo["products"].length - 1)})
      searchSaleInfo('products', saleFormInfo["products"].slice(0, saleFormInfo["products"].length - 1))
    }
  };

  function handleProductInfoChange(index: number, field: keyof ProductInfo, value: string ) {
    setProductInfo((prevProductInfo) => {
      var newProductInfo = [...prevProductInfo];
      newProductInfo[index][field] = value;

      searchSaleInfo('products', newProductInfo);

      setSaleFormInfo({...saleFormInfo, products: newProductInfo})

      return newProductInfo;
    });

  };
  
  // -------------------------------------------------------------------------------------------------------<


  const initProduct: ProductProps = {
    brand: {
      id: '',
      name: ''
    },
    brand_Id: '',
    color: '',
    condition: '',
    cost: 0,
    gender: '',
    id: '',
    price: 0,
    size: '',
    productType: {
      id: '',
      style: '',
      typeName: ''
    },
    type_Id: ''
  }

  const { clients, events, products, sales, sellers } = useApi()
  
  const [ saleFormInfo, setSaleFormInfo] = useState<SaleFormProps>({
    client_id: "",
    event_id: "",
    seller_id: "",
    products: [],
  })

  const [ resumeInfo, setResumeInfo] = useState<AddSaleResumeProps>({
    client: undefined,
    seller: undefined,
    event: undefined,
    products: []
  })

  function searchSaleInfo(field: string, value: string | ProductInfo[] ) {
    switch (field) {
      case 'products':{
        let newProducts: {
          data: ProductProps;
          qtd: string;
      }[] = []

        for (const product of (value as ProductInfo[])) {
          console.log(product)
          const prod = products.find(prod => prod.id === product.product_id)

          if (prod) {

            const indexInResume = newProducts.findIndex(prod => prod.data.id === product.product_id)
            if (indexInResume >= 0) {
              newProducts[indexInResume] = {...newProducts[indexInResume], qtd: (parseInt(newProducts[indexInResume]['qtd']) + parseInt(product.qtd)).toString()}
              console.log('Updating Products in Resume')
              console.log(newProducts)
              continue
            }

            newProducts.push({data: prod, qtd: product.qtd})
            console.log('Adding Products in Resume')
            console.log(newProducts)
            continue
          }
        }

        setResumeInfo({...resumeInfo, products: newProducts})
        break;
      }
      case 'client_id':{
        const client = clients.find(client => client.id === value)
        if(client) setResumeInfo({...resumeInfo, client: client})

        break;
      }
      case 'event_id':{
        const event = events.find(event => event.id === value)
        if(event) setResumeInfo({...resumeInfo, event: event})
        console.log(`Busca concluida de evento: ${event}`)

        break;
      }
      case 'seller_id':{
        const seller = sellers.find(seller => seller.id === value)
        if(seller) setResumeInfo({...resumeInfo, seller: seller})
        console.log(`Busca concluida de vendedor: ${seller}`)

        break;
      }
      default:
        break;
    }
  }

  // -------------> ON CHANGE FUNCTIONS ------------
  function handleChangeClientId(e: React.ChangeEvent<HTMLInputElement>) {
    setSaleFormInfo({...saleFormInfo, client_id: e.target.value})

    searchSaleInfo('client_id', e.target.value)
  }

  function handleChangeSellerId(e: React.ChangeEvent<HTMLInputElement>) {
    setSaleFormInfo({...saleFormInfo, seller_id: e.target.value})

    searchSaleInfo('seller_id', e.target.value)
  }

  function handleChangeEventId(e: React.ChangeEvent<HTMLInputElement>) {
    setSaleFormInfo({...saleFormInfo, event_id: e.target.value})

    searchSaleInfo('event_id', e.target.value)
  }

  // -------------------------------------------------------------------------------------------------------<

  
  return (
    <>
      <main className="w-full h-full p-8">
        <h1 className="font-extrabold text-accent text-6xl mb-8">ADICIONAR VENDA</h1>
        <form action="" className="flex flex-col">
          <div className="flex gap-8 mb-12">
            <div className="flex flex-col w-full">
              <label htmlFor="">Id do Cliente</label>
              <input
                required
                className="border-2 px-2 py-1 rounded-md"
                type="text"
                onChange={handleChangeClientId}
              />
            </div>

            <div className="flex flex-col w-full">
              <label htmlFor="">Id do Vendedor</label>
              <input
                required
                className="border-2 px-2 py-1 rounded-md"
                type="text"
                onChange={handleChangeSellerId}
              />
            </div>

            <div className="flex flex-col w-full">
              <label htmlFor="">Id do Evento</label>
              <input
                required
                className="border-2 px-2 py-1 rounded-md"
                type="text"
                onChange={handleChangeEventId}
              />
            </div>
          </div>

          <div className="flex flex-col gap-8 justify-center shadow-lg p-8">
            <h2 className="font-extrabold text-accent text-4xl">PRODUTOS</h2>
            <div className="grid grid-cols-4 gap-8 w-full items-end">
              {productInfo.map((product, index) => (
                <div key={index} className="flex flex-col w-full">
                  <h3 className="font-extrabold text-accent text-lg">PRODUTO {index + 1}</h3>
                  <label>
                    Id do Produto
                  </label>
                  <input
                    required
                    className="border-2 px-2 py-1 rounded-md"
                    type="text"
                    value={product.product_id}
                    onChange={(e) => handleProductInfoChange(index, 'product_id', e.target.value)}
                  />
                  <label>
                    Quantidade
                  </label>
                  <input
                    required
                    className="border-2 px-2 py-1 rounded-md"
                    type="number"
                    min={1}
                    value={product.qtd}
                    onChange={(e) => handleProductInfoChange(index, 'qtd', e.target.value)}
                  />
                </div>
              ))}

              {/* BOTAO DE ADICIONAR PRODUTO */}
              <div className="flex flex-col gap-4 max-w-[2rem]">
                <div onClick={() => handleNumProductsChange(-1)} className="p-2 bg-accent rounded-full hover:opacity-80 cursor-pointer">
                  <Minus
                    width={16}
                    height={16}
                    color="#fff"
                  />
                </div>
                <div onClick={() => handleNumProductsChange(1)} className="p-2 bg-accent rounded-full hover:opacity-80 cursor-pointer">
                  <Plus
                    width={16}
                    height={16}
                    color="#fff"
                  />
                </div>
              </div>

            </div>
          </div>

          <AddSaleResume
            client={resumeInfo.client}
            seller={resumeInfo.seller}
            event={resumeInfo.event}
            products={resumeInfo.products}
          />
        </form>
      </main>
    </>
  )
}