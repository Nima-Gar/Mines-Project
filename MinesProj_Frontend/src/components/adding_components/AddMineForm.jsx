import React, { useEffect, useRef, useState } from 'react'
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom'
import SimpleReactValidator from 'simple-react-validator'

import AddNumberModal from './AddNumberModal'
import ConfirmDeletionModal from './ConfirmDeletionModal'
import { addNumber } from '../../services/phoneNumbersService'
import { addMine } from '../../services/minesService'
import numbersContext from '../contexts/numbersContext'
import { useContext } from 'react'
import dropdownDictsContext from '../contexts/dropdownDictsContext'

let id = 1

const AddMineForm = () => {
  const [phonenums, setPhonenums] = useState([])
  const [idToDelete, setIdToDelete] = useState()
  const [screenWidth, setScreenWidth] = useState(window.innerWidth)
  const [, setForceUpdate] = useState(false)

  const [mineName, setMineName] = useState('')
  const [computerCode, setComputerCode] = useState('')
  const [ownershipTypeId, setOwnershipTypeId] = useState()
  const [provinceId, setProvinceId] = useState()
  const [countyId, setCountyId] = useState()
  const [address, setAddress] = useState('')
  const [geoghraphicPosition, setGeoghraphicPosition] = useState('')
  const [investmentAmount, setInvestmentAmount] = useState(0)
  const [degree, setDegree] = useState(0)
  const [area, setArea] = useState(0)
  const [employmentCommitment, setEmploymentCommitment] = useState(false)
  const [MineTypeId, setMineTypeId] = useState()
  const [statusId, setStatusId] = useState()

  const {
    numTypesDictionary,
    ownershipTypesDictionary,
    provincesDictionary,
    countiesDictionary,
    mineTypesDictionary,
    statusesDictionary,
    countyToProvinceId_Dictionary,
  } = useContext(dropdownDictsContext)

  const navigate = useNavigate()

  const firstInput = useRef(null)
  const validator = useRef(
    new SimpleReactValidator({
      messages: {
        required: 'این فیلد الزامی است',
        min: 'این فیلد نباید کمتر از :attribute باشد',
        max: 'این فیلد نباید بیشتر از :attribute باشد',
        numeric: 'این فیلد عددی است',
        between: 'مقدار فیلد باید بین :attribute باشد',
        integer: 'یک مقدار باید برای این فیلد انتخاب شود',
      },
      element: (message) => (
        <p style={{ color: 'red', paddingBottom: '0', marginBottom: '0' }}>
          {message}
        </p>
      ),
    })
  )

  useEffect(() => {
    firstInput.current.focus()
  }, [])

  useEffect(() => {
    window.addEventListener('resize', () => setScreenWidth(window.innerWidth))
    return () => {
      window.removeEventListener('resize', () =>
        setScreenWidth(window.innerWidth)
      )
    }
  }, [screenWidth])

  const fieldsAreValid = () =>
    validator.current.fieldValid('100 و 120') &&
    validator.current.fieldValid('ownershipType') &&
    validator.current.fieldValid('province') &&
    validator.current.fieldValid('county')

  const handleNewNumber = (number, phoneNumTypeRefId) => {
    const phonenumber = {
      tempId: id++,
      number,
      phoneNumTypeRefId,
      mineRefId: 0,
    }
    setPhonenums([...phonenums, phonenumber])
    toast.success('شماره با موفقیت افزوده شد.')
  }

  const deleteNumber = () => {
    const filteredPhonenums = phonenums.filter(
      (phonenum) => phonenum.tempId !== idToDelete
    )
    setPhonenums(filteredPhonenums)
    toast.error('شماره از لیست حذف شد.')
    setIdToDelete()
  }

  const handleSubmit = async () => {
    if (validator.current.allValid()) {
      const mine = {
        name: mineName,
        computerCode,
        ownershipTypeRefId: ownershipTypeId,
        provinceRefId: provinceId,
        countyRefId: countyId,
        address,
        geoghraphicPosition,
        investmentAmount,
        degree,
        area,
        employmentCommitment,
        mineTypeRefId: MineTypeId,
        statusRefId: statusId,
      }

      try {
        const { data, status } = await addMine(mine)
        if (status === 201) {
          const finalizedPhonenums = finalizePhonenums(data.id)

          const numbersAddedSuccessfully = addNumbers(finalizedPhonenums)
          if (numbersAddedSuccessfully) {
            toast.success('معدن با موفقیت ثبت شد.', { autoClose: 1000 })
            setTimeout(() => {
              navigate('/', { replace: true })
            }, 1500)
          } else
            toast.error(
              'معدن با موفقیت ثبت شد اما مشکلی در ثبت شماره تلفن ها پیش آمد'
            )
        }
      } catch (exception) {
        toast.error('مشکلی در افزودن معدن به وجود آمد. لطفا مجددا تلاش کنید')
        console.log(exception)
      }
    } else {
      validator.current.showMessages()
      setForceUpdate(true)
    }
  }

  const finalizePhonenums = (mineRefId) => {
    // deleting temporary IDs and setting newly created mine as phonenumbers related mine
    return phonenums.map((phonenum) => {
      const { tempId, ...filteredPhonenum } = phonenum
      filteredPhonenum.mineRefId = mineRefId
      return filteredPhonenum
    })
  }

  const addNumbers = async (phonenumsToPost) => {
    let createdAll = true
    await Promise.all(
      phonenumsToPost.map(async (phonenum) => {
        try {
          const { status } = await addNumber(phonenum)
          console.log('posted a number in map function! status code:', status)
          createdAll = status === 201 && createdAll
        } catch (exception) {
          console.log(exception)
        }
      })
    )
    console.log('returning final result')
    return createdAll
  }

  return (
    <numbersContext.Provider
      value={{
        numTypesDictionary,
        idToDelete,
        setIdToDelete,
        handleNewNumber,
        deleteNumber,
      }}
    >
      <div id="addMineForm" className="py-md-4 px-md-3 py-2 px-sm-4 px-2">
        <section className={screenWidth >= 768 ? 'row' : ''}>
          <div className="col-md-5 col-12 d-md-flex justify-content-center align-items-center p-md-0 px-0 py-3">
            <div className="col-1" />
            <div className="col-6 d-flex flex-column">
              <strong className="form-section-title">اطلاعات پایه</strong>
              <span className="form-section-description">
                اطلاعات پایه معدن ضروری می باشد
              </span>
            </div>
          </div>

          <form
            className="col-md-6 col-12 p-md-0"
            onSubmit={(e) => {
              e.preventDefault()
              handleSubmit()
            }}
          >
            <div className="form-group">
              <input
                ref={firstInput}
                type="text"
                className="form-control form-control-sm"
                placeholder="نام معدن"
                value={mineName}
                onChange={(e) => {
                  setMineName(e.target.value)
                }}
                onBlur={() =>
                  validator.current.showMessageFor('3 کاراکتر داشته')
                }
              />
              {validator.current.message(
                '3 کاراکتر داشته',
                mineName.trim(),
                'required|min:3'
              )}
            </div>

            <div className="row" id="inputsGroup">
              <div className="form-group col-lg col-6">
                <input
                  type="text"
                  className="form-control form-control-sm"
                  placeholder="کد کامپیوتری"
                  value={computerCode}
                  onChange={(e) => {
                    setComputerCode(e.target.value)
                  }}
                  onBlur={() => validator.current.showMessageFor('100 و 120')}
                />
                {validator.current.message(
                  '100 و 120',
                  computerCode.trim(),
                  'required|numeric|between:100,120,num'
                )}{' '}
              </div>

              <div className="form-group col-lg col-6">
                <select
                  className={`form-control px-1 ${
                    fieldsAreValid() ? 'h-100' : ''
                  }`}
                  value={ownershipTypeId ? ownershipTypeId : -1}
                  onChange={(e) => {
                    setOwnershipTypeId(parseInt(e.target.value))
                  }}
                >
                  <option value="-1" disabled>
                    نوع مالکیت
                  </option>
                  {Object.keys(ownershipTypesDictionary).map((typeId) => (
                    <option key={typeId} value={typeId}>
                      {ownershipTypesDictionary[typeId]}
                    </option>
                  ))}
                </select>
                {validator.current.message(
                  'ownershipType',
                  ownershipTypeId,
                  'required'
                )}
              </div>

              <div className="form-group col-lg col-6">
                <select
                  className={`form-control ${fieldsAreValid() ? 'h-100' : ''}`}
                  id="provinceSelect"
                  value={provinceId ? provinceId : -1}
                  onChange={(e) => setProvinceId(parseInt(e.target.value))}
                >
                  <option value="-1" disabled>
                    استان
                  </option>
                  {Object.keys(provincesDictionary).map((typeId) => (
                    <option key={typeId} value={typeId}>
                      {provincesDictionary[typeId]}
                    </option>
                  ))}
                </select>
                {validator.current.message('province', provinceId, 'required')}
              </div>

              <div className="form-group col-lg col-6">
                <select
                  className={`form-control ${fieldsAreValid() ? 'h-100' : ''}`}
                  value={countyId ? countyId : -1}
                  onChange={(e) => setCountyId(parseInt(e.target.value))}
                >
                  <option value="-1" disabled>
                    شهرستان
                  </option>
                  {provinceId ? (
                    Object.keys(countiesDictionary).map((typeId) =>
                      countyToProvinceId_Dictionary[typeId] === provinceId ? (
                        <option key={typeId} value={typeId}>
                          {countiesDictionary[typeId]}
                        </option>
                      ) : null
                    )
                  ) : (
                    <>
                      <option className="d-block" disabled>
                        ابتدا استان را
                      </option>
                      <option className="d-block" disabled>
                        مشخص کنید
                      </option>
                    </>
                  )}
                </select>
                {validator.current.message('county', countyId, 'required')}
              </div>
            </div>

            <div className="form-group">
              <input
                type="text"
                className="form-control form-control-sm"
                placeholder="آدرس معدن"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
                onBlur={() => validator.current.showMessageFor('address')}
              />
              {validator.current.message('address', address.trim(), 'required')}{' '}
            </div>

            <div className="form-group">
              <input
                type="text"
                className="form-control form-control-sm"
                placeholder="موقعیت جغرافیایی معدن"
                value={geoghraphicPosition}
                onChange={(e) => setGeoghraphicPosition(e.target.value)}
                onBlur={() =>
                  validator.current.showMessageFor('geoghraphicPosition')
                }
              />
              {validator.current.message(
                'geoghraphicPosition',
                geoghraphicPosition,
                'required|string'
              )}{' '}
            </div>

            <div
              className="row d-flex justify-content-between"
              id="textInputGroup"
            >
              <div className="form-group col">
                <input
                  type="text"
                  className="form-control form-control-sm"
                  placeholder="میزان اسمی سرمایه گذاری"
                  value={investmentAmount ? investmentAmount : ''}
                  onChange={(e) => setInvestmentAmount(e.target.value)}
                  onBlur={() => validator.current.showMessageFor('1000')}
                />
                {validator.current.message(
                  '1000',
                  investmentAmount,
                  'required|numeric|min:1000,num'
                )}{' '}
              </div>

              <div className="form-group col">
                <input
                  type="text"
                  className="form-control form-control-sm"
                  placeholder="درجه"
                  value={degree ? degree : ''}
                  onChange={(e) => setDegree(e.target.value)}
                  onBlur={() => validator.current.showMessageFor('1 و 5')}
                />
                {validator.current.message(
                  '1 و 5',
                  degree,
                  'required|numeric|between:1,5,num'
                )}{' '}
              </div>

              <div className="form-group col-lg">
                <input
                  type="text"
                  className="form-control form-control-sm"
                  placeholder="مساحت محدوده (کیلومتر مربع)"
                  value={area ? area : ''}
                  onChange={(e) => setArea(e.target.value)}
                  onBlur={() => validator.current.showMessageFor('area')}
                />
                {validator.current.message('area', area, 'required|numeric')}{' '}
              </div>
            </div>

            <div className="row">
              <div className="col-3 pt-1">
                <button
                  className="btn btn-sm btn-primary rounded-pill w-100"
                  type="button"
                  data-toggle="modal"
                  data-target="#addNumModal"
                >
                  <i className="fa fa-plus" aria-hidden="true" />
                  افزودن شماره
                </button>
              </div>
              <table id="phonenumsTable" className="table table-hover rounded">
                <thead>
                  <tr>
                    <th scope="col">نوع</th>
                    <th scope="col">شماره</th>
                    <th scope="col">حذف</th>
                  </tr>
                </thead>
                <tbody>
                  {phonenums
                    ? phonenums.map((phonenum) => (
                        <tr key={phonenum.tempId}>
                          <td>
                            {numTypesDictionary[phonenum.phoneNumTypeRefId]}
                          </td>
                          <td>{phonenum.number}</td>
                          <td>
                            <button
                              className="btn btn-sm btn-danger fa fa-trash"
                              type="button"
                              data-toggle="modal"
                              data-target="#confirmDeleteModal"
                              onClick={(e) => {
                                e.preventDefault()
                                setIdToDelete(phonenum.tempId)
                              }}
                            />
                          </td>
                        </tr>
                      ))
                    : null}
                </tbody>
              </table>
              <div className="w-100 mb-2 d-flex justify-content-center">
                {validator.current.message(
                  'numbersList',
                  phonenums,
                  'required'
                )}{' '}
              </div>
            </div>

            <div className="row" id="lastRow">
              <div className="form-group col-4 col-4 px-sm-3">
                <select
                  className="form-control"
                  value={MineTypeId ? MineTypeId : -1}
                  onChange={(e) => setMineTypeId(parseInt(e.target.value))}
                >
                  <option value="-1" disabled>
                    نوع معدن
                  </option>
                  {Object.keys(mineTypesDictionary).map((typeId) => (
                    <option key={typeId} value={typeId}>
                      {mineTypesDictionary[typeId]}
                    </option>
                  ))}
                </select>
                {validator.current.message('mineType', MineTypeId, 'required')}{' '}
              </div>

              <div className="form-group col-4 px-md-1 px-2">
                <select
                  className="form-control"
                  value={statusId ? statusId : -1}
                  onChange={(e) => setStatusId(parseInt(e.target.value))}
                  id="statusSelect"
                >
                  <option value="-1" disabled>
                    وضعیت
                  </option>
                  {Object.keys(statusesDictionary).map((typeId) => (
                    <option key={typeId} value={typeId}>
                      {statusesDictionary[typeId]}
                    </option>
                  ))}
                </select>
                {validator.current.message('status', statusId, 'required')}{' '}
              </div>

              <div className="form-check col-4 px-sm-3 px-1">
                <input
                  type="checkbox"
                  className="form-check-input"
                  id="exampleCheck1"
                  value={employmentCommitment}
                  onChange={(e) => setEmploymentCommitment(e.target.checked)}
                />
                <label
                  className="form-check-label pt-1 px-3"
                  htmlFor="exampleCheck1"
                >
                  تعهد اشتغال دارد
                </label>
              </div>
            </div>
            <div className="row d-flex justify-content-center pt-3 pb-2">
              <button
                type="submit"
                className="btn btn-success mx-md-3 px-5 pt-1"
              >
                ثبت معدن
              </button>
            </div>
          </form>
        </section>
      </div>

      {numTypesDictionary ? <AddNumberModal /> : null}
      <ConfirmDeletionModal />
    </numbersContext.Provider>
  )
}

export default AddMineForm
