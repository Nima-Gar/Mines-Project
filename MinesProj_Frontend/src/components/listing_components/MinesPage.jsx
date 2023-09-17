import React from 'react'

import { getMines } from '../../services/minesService'
import { useEffect, useState } from 'react'
import NumbersDemoModal from './NumbersDemoModal'
import { useNavigate } from 'react-router-dom'
import PaginatedMinesTable from './PaginatedMinesTable'
import numbersContext from '../contexts/numbersContext'

const MinesPage = () => {
  const navigate = useNavigate()

  const [mines, setMines] = useState([])
  const [currentMinePhonenums, setCurrentMinePhonenums] = useState([])
  const [minesPerPage, setMinesPerPage] = useState(0)

  useEffect(() => {
    fillMinesList()
  }, [])

  const fillMinesList = async () => {
    let minesList = await getMines()
    minesList = minesList.map((mine, index) => ({ tempId: index + 1, ...mine }))
    setMines(minesList)
  }

  return (
    <numbersContext.Provider value={{ setCurrentMinePhonenums }}>
      <div className="row d-flex flex-md-row flex-column align-items-center py-2">
        <select
          className="form-control order-md-0 order-1 col-md-2 col-5 mr-md-3 mx-0"
          value={minesPerPage ? minesPerPage : -1}
          onChange={(e) => setMinesPerPage(parseInt(e.target.value))}
        >
          <option value="-1" disabled>
            تعداد نتایج
          </option>
          <option value={3}>3</option>
          <option value={4}>4</option>
          <option value={5}>5</option>
        </select>
        <div className="infoSection bg-white order-0 mx-auto py-1 px-1 d-flex align-items-center">
          <div className="iconDiv px-3 py-3 rounded-circle d-flex justify-content-center">
            <i className="icon fa fa-th-list"></i>
          </div>
          <div className="w-100 d-flex justify-content-center">
            <p className="infoSectionTitle">تعداد معادن: {mines.length}</p>
          </div>
        </div>
        <button
          className="btn btn-info h-50 order-md-0 order-0 ml-md-3 mx-0 my-md-auto mb-3"
          onClick={() => navigate('/add-mine', { replace: false })}
        >
          <i className="fa fa-plus ml-2" />
          افزودن معدن
        </button>
      </div>

        <div
          id="minesTable"
          className="row col-12 py-2 px-0 mx-0 justify-content-center"
        >
          <PaginatedMinesTable mines={mines} setMines={setMines} minesPerPage={minesPerPage ? minesPerPage : 4} />
          <NumbersDemoModal currentMinePhonenums={currentMinePhonenums} />
        </div>
    </numbersContext.Provider>
  )
}

export default MinesPage
