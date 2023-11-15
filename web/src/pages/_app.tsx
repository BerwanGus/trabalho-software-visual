import { AppProps } from 'next/app';
import "../app/global/globals.css";

import { Montserrat } from 'next/font/google'
import { Router } from 'react-router-dom';
import { ApiProvider } from '@/app/context/api.context';

const montserrat = Montserrat({ subsets: ['latin'] })

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <ApiProvider>
      <Component {...pageProps} className={montserrat.className}/>
    </ApiProvider>
  );
}

export default MyApp;