import { AppProps } from 'next/app';
import "../app/global/globals.css";

import { Montserrat } from 'next/font/google'

const montserrat = Montserrat({ subsets: ['latin'] })

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <Component {...pageProps} className={montserrat.className}/>
  );
}

export default MyApp;