import React, { useState } from 'react'
import ReactPaginate from 'react-paginate'

import PopoverDialog from './../common/PopoverDialog'
import MinesTable from './MinesTable'
import minesListContext from '../contexts/minesListContext'

const PaginatedMinesTable = ({ mines, setMines, minesPerPage }) => {

  const [mineOffset, setMineOffset] = useState(0)
  const endOffset = mineOffset + minesPerPage

  const currentPageMines = mines.slice(mineOffset, endOffset)
  const pageCount = Math.ceil(mines.length / minesPerPage)

  const handlePageClick = (event) => {
    const newOffset = event.selected * minesPerPage
    setMineOffset(newOffset)
  }

  return (
    <minesListContext.Provider value={{mines, setMines, currentPageMines}}>
      <MinesTable/>
      <ReactPaginate
        className=""
        containerClassName="pageBtnsContainer d-flex justify-content-between align-items-center px-0 mt-1"
        pageClassName="pageBtn py-1 rounded mr-1"
        activeClassName="activePageBtn"
        previousClassName="moveBtn"
        nextClassName="moveBtn px-1"
        breakLabel="..."
        breakClassName="mr-1"
        previousLabel="<"
        nextLabel=">"
        onPageChange={handlePageClick}
        pageRangeDisplayed={3}
        pageCount={pageCount}
        renderOnZeroPageCount={null}
      />
      <PopoverDialog />
    </minesListContext.Provider>
  )
}

export default PaginatedMinesTable
