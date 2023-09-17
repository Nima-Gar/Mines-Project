import React from 'react'
import AddMineForm from './AddMineForm'
import { useNavigate } from 'react-router-dom'
import DropdownDictsProvider from './../common/hoc/DropdownDictsProvider'

const AddSection = () => {
  const navigate = useNavigate()

  return (
    <div id="addSection">
      <header className="pt-3 pb-2">
        <button onClick={() => navigate('/', { replace: false })}>
          <i className="fa fa-arrow-right mb-2" />
        </button>
        <h5 id="header">افزودن معدن</h5>
      </header>
      <DropdownDictsProvider>
        <AddMineForm />
      </DropdownDictsProvider>
    </div>
  )
}

export default AddSection
