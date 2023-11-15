import { Plus } from "react-feather";
import { redirect } from 'next/navigation';
import Link from "next/link";

export function AddSalePopup() {
  return (
    <div>
      <Link
        href='/add_sale'
        className="fixed bottom-5 right-5 p-4 bg-accent rounded-full hover:opacity-80 cursor-pointer transition-all"
      >
        <Plus 
          color="#fff"
          href="/add_sale"
        />
      </Link>
    </div>
  )
}