import React, { useRef, useContext } from 'react'
import numbersContext from '../contexts/numbersContext'

const ConfirmDeletionModal = () => {
  const closeBtnRef = useRef(null)
  const { setIdToDelete, deleteNumber } = useContext(numbersContext)

  return (
    <div
      className="modal fade"
      id="confirmDeleteModal"
      tabIndex="-1"
      role="dialog"
      aria-labelledby="confirmDeleteModalLabel"
      aria-hidden="true"
    >
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header d-flex justify-content-start align-items-center px-0 bg-danger text-white">
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
              حذف شماره
            </h6>
          </div>
          <div className="modal-body">
            <div className="px-1 pb-3">
              <h6>آیا از حذف شماره اطمینان دارید؟</h6>
            </div>
            <form
              className="form-group d-flex flex-column align-items-center"
              id="addNumForm"
              onSubmit={(e) => {
                e.preventDefault()
                deleteNumber()
                closeBtnRef.current.click()
              }}
            >
              <div className="d-flex">
                <button
                  type="submit"
                  className="btn btn-info rounded pull-left px-5 mx-2"
                >
                  بله
                </button>
                <button
                  type="button"
                  className="btn btn-danger rounded pull-left px-5 mx-2"
                  onClick={() => {
                    setIdToDelete()
                    closeBtnRef.current.click()
                  }}
                >
                  خیر
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}

export default ConfirmDeletionModal
