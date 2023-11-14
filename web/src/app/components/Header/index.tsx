interface MainHeaderProps {
  functions: {
    name: string;
    handle: () => void;
  }[];
}

export function MainHeader({functions}: MainHeaderProps) {
  return (
    <header className='flex w-full bg-accent text-body-white p-10 drop-shadow-xl'>
      <nav className='flex w-full justify-center items-center text-2xl font-extrabold lowercase'>
        
        <div className='flex gap-24 mr-32'>
          {
            functions.slice(0, Math.floor(functions.length/2)).map((link) => 
              <a key={link.name} className='cursor-pointer hover:opacity-80 transition-all' onClick={link.handle}>{link.name}</a>
            )
          }
        </div>
        <div className='flex gap-24 ml-32'>
          {
            functions.slice(Math.floor(functions.length/2)).map((link) => 
              <a key={link.name} className='cursor-pointer hover:opacity-80 transition-all' onClick={link.handle}>{link.name}</a>
            )
          }
        </div>
        <a className='cursor-pointer hover:opacity-80 transition-all' href="#">Logo</a>
      </nav>
    </header>
  )
}