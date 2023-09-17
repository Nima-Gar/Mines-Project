import React from 'react'

const NumbersDemoModal = ({ currentMinePhonenums }) => {
  const localPhonenums = currentMinePhonenums.filter(
    (phonenum) => phonenum.type === 'ثابت'
  )
  const cellPhonenums = currentMinePhonenums.filter(
    (phonenum) => phonenum.type === 'موبایل'
  )
  return (
    <>
      <div
        className="modal fade"
        id="numbersDemoModal"
        role="dialog"
        aria-labelledby="numbersDemoModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header d-flex justify-content-start bg-info text-white">
              <button
                type="button"
                className="close text-white ml-0 pt-3"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">&times;</span>
              </button>
              <h5 className="modal-title" id="numbersDemoModalLabel">
                لیست شماره ها
              </h5>
            </div>
            <div className="modal-body">
              <p>ثابت:</p>
              {localPhonenums.map((phoneNumber, index) => (
                <p key={index}>{phoneNumber.number}</p>
              ))}
              <p>همراه:</p>
              {cellPhonenums.map((phoneNumber, index) => (
                <p key={index}>{phoneNumber.number}</p>
              ))}
            </div>
          </div>
        </div>
      </div>
    </>
  )
}

export default NumbersDemoModal
