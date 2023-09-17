import React from 'react'
import { useContext } from 'react'

import numbersContext from '../contexts/numbersContext'

const MinesList = ({currentPageMines}) => {
  const { setCurrentMinePhonenums } = useContext(numbersContext)

  return (
    <>
      {currentPageMines.map((mine, index) => (
        <tr key={index}>
          <th scope="row">{mine.tempId}</th>
          <td>{mine.name}</td>
          <td>{mine.computerCode}</td>
          <td>{mine.ownershipType}</td>
          <td>{mine.province}</td>
          <td>{mine.county}</td>
          <td>{mine.address}</td>
          <td>{mine.geoghraphicPosition}</td>
          <td>{mine.investmentAmount}</td>
          <td>{mine.degree}</td>
          <td>{mine.area}</td>
          <td>{mine.mineType}</td>
          <td
            className={`alert ${
              mine.status === 'فعال' ? 'alert-info' : 'alert-primary'
            }`}
          >
            {mine.status}
          </td>
          <td>{mine.employmentCommitment ? 'دارد' : 'ندارد'}</td>
          <td>
            <button
              type="button"
              className="btn btn-primary"
              data-toggle="modal"
              data-target="#numbersDemoModal"
              onClick={() => setCurrentMinePhonenums(mine.phoneNumbers)}
            >
              نمایش
            </button>
          </td>
        </tr>
      ))}
    </>
  )
}

export default MinesList
