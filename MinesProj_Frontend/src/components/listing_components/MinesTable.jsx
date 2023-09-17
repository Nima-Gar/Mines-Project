import { useContext } from 'react'

import MinesList from './MinesList'
import DropdownDictsProvider from './../common/hoc/DropdownDictsProvider'
import MinesHeaders from './MinesHeaders'
import minesListContext from '../contexts/minesListContext'
import { useState } from 'react'

const MinesTable = () => {
  const [renderNotFoundMessage, setRenderNotFoundMessage] = useState(false)
  const { currentPageMines } = useContext(minesListContext)

  setTimeout(() => setRenderNotFoundMessage(true), 2500)

  return (
    <table className="table table-light table-hover table-responsive rounded">
      <thead className="thead-dark">
        <DropdownDictsProvider>
          <MinesHeaders />
        </DropdownDictsProvider>
      </thead>
      <tbody>
        <MinesList currentPageMines={currentPageMines} />
      </tbody>
      {currentPageMines.length === 0 && renderNotFoundMessage ? (
        <div className="notFoundMessageContainer py-5">
          <p className="w-25 my-0 h5">
            <i className="fa fa-times align-middle ml-1" />
            <span>معدنی یافت نشد</span>
            <i className="fa fa-times align-middle mr-1" />
          </p>
        </div>
      ) : null}
    </table>
  )
}

export default MinesTable
