import React, { useRef, useContext, useState, useEffect } from 'react'
import SimpleReactValidator from 'simple-react-validator'

import numbersContext from '../contexts/numbersContext'

const AddNumberModal = () => {
  const [numTypeRefId, setNumTypeRefId] = useState()
  const [number, setNumber] = useState('')
  const [correctNumForm, setCorrectNumForm] = useState(false)
  const [, setForceUpdate] = useState(false)

  const { numTypesDictionary, handleNewNumber } = useContext(numbersContext)

  const closeBtnRef = useRef(null)
  const validator = useRef(
    new SimpleReactValidator({
      messages: {
        required: 'این فیلد الزامی است',
        min: 'شماره تلفن نمیتواند عددی منفی باشد!',
        size: 'شماره تلفن عددی 11 رقمی می باشد',
        numeric: 'این فیلد عددی است',
      },
      element: (message) => (
        <p
          style={{
            color: 'red',
            paddingBottom: '0',
            marginBottom: '0',
            fontSize: '.7rem',
          }}
        >
          {message}
        </p>
      ),
    })
  )

  useEffect(() => {
    if (number.charAt(0) === '0' && !correctNumForm) setCorrectNumForm(true)
    else if (number.charAt(0) !== '0' && correctNumForm)
      setCorrectNumForm(false)
  }, [number])

  const handleSubmit = () => {
    if (validator.current.allValid() && correctNumForm) {
      handleNewNumber(number, numTypeRefId)
      closeBtnRef.current.click()
      setCorrectNumForm(false)
      setForceUpdate(false)
      setNumTypeRefId()
      setNumber('')
      validator.current.hideMessages()
    } else {
      validator.current.showMessages()
      // to force rendering again
      setForceUpdate(true)
    }
  }

  return (
    <div
      className="modal fade"
      id="addNumModal"
      tabIndex="-1"
      role="dialog"
      aria-labelledby="addNumModalLabel"
      aria-hidden="true"
    >
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header d-flex justify-content-start align-items-center px-0 bg-info text-white">
            <button
              ref={closeBtnRef}
              type="button"
              className="close mx-0 text-white"
              id="modalCloseBtn"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
            <h6 className="modal-title" id="exampleModalLabel">
              افزودن شماره
            </h6>
          </div>
          <div className="modal-body">
            <form
              className="form-group d-flex flex-column align-items-center"
              id="addNumForm"
              onSubmit={(e) => {
                e.preventDefault()
                handleSubmit()
              }}
            >
              <div className=" col-md-7 col-sm-8 col-11 px-0 d-flex flex-column align-items-center mb-2">
                <select
                  className="form-control"
                  value={numTypeRefId ? numTypeRefId : -1}
                  onChange={(e) => setNumTypeRefId(e.target.value)}
                >
                  <option value="-1" disabled>
                    نوع شماره
                  </option>
                  {Object.keys(numTypesDictionary).map((typeId) => (
                    <option key={typeId} value={typeId}>
                      {numTypesDictionary[typeId]}
                    </option>
                  ))}
                </select>
                {validator.current.message('numType', numTypeRefId, 'required')}
              </div>

              <div className="col-md-7 col-sm-8 col-11 px-0 d-flex flex-column align-items-center">
                <input
                  type="text"
                  value={number}
                  onChange={(e) => setNumber(e.target.value)}
                  className="form-control"
                  placeholder="شماره"
                />
                {validator.current.message(
                  'number',
                  number,
                  'required|numeric|min:0,num|size:11'
                )}{' '}
                {validator.current.fieldValid('number') && !correctNumForm ? (
                  <p
                    style={{
                      color: 'red',
                      paddingBottom: '0',
                      marginBottom: '0',
                      fontSize: '.7rem',
                    }}
                  >
                    شماره تلفن باید با 0 شروع شود
                  </p>
                ) : null}
              </div>
              <button type="submit" className="btn btn-info rounded pull-left">
                ثبت شماره
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}

export default AddNumberModal
