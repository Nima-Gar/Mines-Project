import React from 'react'

import MinesPage from './MinesPage';

const ListSection = () => {
  return (
    <div id="listSection">
      <header className="pt-3 pb-2">
        <h4 id="header">لیست معادن</h4>
      </header>
      <MinesPage />
    </div>
  )
}

export default ListSection;