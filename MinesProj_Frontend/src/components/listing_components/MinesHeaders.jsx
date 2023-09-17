import React from 'react'
import { useState, useContext, useEffect } from 'react'

import PopoverDialog from './../common/PopoverDialog'
import dropdownDictsContext from './../contexts/dropdownDictsContext'
import minesListContext from '../contexts/minesListContext'
import { getFilteredtMines } from '../../services/minesService'

const MinesHeaders = () => {
  let popoverId = 1

  //Filters
  const [Name, setName] = useState('')
  const [ApplyNameFilter, setApplyNameFilter] = useState(false)
  const [Computercode, setComputercode] = useState('')
  const [ApplyComputerCodeFilter, setApplyComputerCodeFilter] = useState(false)
  const [OwnershipTypeRefId, setOwnershipTypeRefId] = useState()
  const [ProvinceRefId, setProvinceRefId] = useState()
  const [CountyRefId, setCountyRefId] = useState()
  const [Address, setAddress] = useState('')
  const [ApplyAddressFilter, setApplyAddressFilter] = useState(false)
  const [GeographicPosition, setGeographicPosition] = useState('')
  const [ApplyGeographicPositionFilter, setApplyGeographicPositionFilter] =
    useState(false)
  const [InvestmentAmountUpperBound, setInvestmentAmountUpperBound] = useState()
  const [InvestmentAmountLowerBound, setInvestmentAmountLowerBound] = useState()
  const [ApplyInvestmentFilter, setApplyInvestmentFilter] = useState(false)
  const [DegreeUpperBound, setDegreeUpperBound] = useState()
  const [DegreeLowerBound, setDegreeLowerBound] = useState()
  const [ApplyDegreeFilter, setApplyDegreeFilter] = useState(false)
  const [AreaUpperBound, setAreaUpperBound] = useState()
  const [AreaLowerBound, setAreaLowerBound] = useState()
  const [ApplyAreaFilter, setApplyAreaFilter] = useState(false)
  const [EmploymentCommitment, setEmploymentCommitment] = useState()
  const [MineTypeRefId, setMineTypeRefId] = useState()
  const [StatusRefId, setStatusRefId] = useState()
  const [PhoneNumber, setPhoneNumber] = useState('')
  const [ApplyPhoneNumberFilter, setApplyPhoneNumberFilter] = useState(false)

  const {
    ownershipTypesDictionary,
    provincesDictionary,
    countiesDictionary,
    mineTypesDictionary,
    statusesDictionary,
    countyToProvinceId_Dictionary,
  } = useContext(dropdownDictsContext)

  const { setMines } = useContext(minesListContext)

  useEffect(() => {
    if (ApplyNameFilter) {
      console.log(Name)
      filterMinesList()
      setApplyNameFilter(false)
    }
  }, [ApplyNameFilter])

  useEffect(() => {
    if (ApplyComputerCodeFilter) {
      console.log(Computercode)
      filterMinesList()
      setApplyComputerCodeFilter(false)
    }
  }, [ApplyComputerCodeFilter])

  useEffect(() => {
    if (ApplyAddressFilter) {
      console.log(Address)
      filterMinesList()
      setApplyAddressFilter(false)
    }
  }, [ApplyAddressFilter])

  useEffect(() => {
    if (ApplyGeographicPositionFilter) {
      console.log(GeographicPosition)
      filterMinesList()
      setApplyGeographicPositionFilter(false)
    }
  }, [ApplyGeographicPositionFilter])

  useEffect(() => {
    if (ApplyPhoneNumberFilter) {
      console.log(PhoneNumber)
      filterMinesList()
      setApplyPhoneNumberFilter(false)
    }
  }, [ApplyPhoneNumberFilter])

  useEffect(() => {
    console.log(OwnershipTypeRefId)
    filterMinesList()
  }, [OwnershipTypeRefId])

  useEffect(() => {
    console.log(ProvinceRefId)
    if(CountyRefId)
      setCountyRefId()
    else
      filterMinesList()
  }, [ProvinceRefId])

  useEffect(() => {
    console.log(CountyRefId)
    filterMinesList()
  }, [CountyRefId])

  useEffect(() => {
    console.log(MineTypeRefId)
    filterMinesList()
  }, [MineTypeRefId])

  useEffect(() => {
    console.log(StatusRefId)
    filterMinesList()
  }, [StatusRefId])

  useEffect(() => {
    if (ApplyInvestmentFilter) {
      console.log(InvestmentAmountLowerBound)
      console.log(InvestmentAmountUpperBound)
      filterMinesList()
      setApplyInvestmentFilter(false)
    }
  }, [ApplyInvestmentFilter])

  useEffect(() => {
    if (ApplyDegreeFilter) {
      console.log(DegreeLowerBound)
      console.log(DegreeUpperBound)
      filterMinesList()
      setApplyDegreeFilter(false)
    }
  }, [ApplyDegreeFilter])

  useEffect(() => {
    if (ApplyAreaFilter) {
      console.log(AreaLowerBound)
      console.log(AreaUpperBound)
      filterMinesList()
      setApplyAreaFilter(false)
    }
  }, [ApplyAreaFilter])

  useEffect(() => {
    console.log(EmploymentCommitment, typeof EmploymentCommitment)
    filterMinesList()
  }, [EmploymentCommitment])

  const filterMinesList = async () => {
    const filters = {
      Name,
      Computercode,
      OwnershipTypeRefId,
      ProvinceRefId,
      CountyRefId,
      Address,
      GeographicPosition,
      InvestmentAmountUpperBound,
      InvestmentAmountLowerBound,
      DegreeUpperBound,
      DegreeLowerBound,
      AreaUpperBound,
      AreaLowerBound,
      EmploymentCommitment,
      MineTypeRefId,
      StatusRefId,
      PhoneNumber,
    }
    const newlyFilteredList = await getFilteredtMines(filters)
    newlyFilteredList = newlyFilteredList.map((mine, index) => ({ tempId: index + 1, ...mine }))
    setMines(newlyFilteredList)
  }

  return (
    <tr>
      <th scope="col">#</th>
      <th scope="col" id="nameCol">
        <PopoverDialog
          id={popoverId++}
          label="نام معدن"
          inputKind="input"
          filter={Name}
          setFilter={setName}
          setApplyFilter={setApplyNameFilter}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="کد کامپیوتری"
          inputKind="input"
          filter={Computercode}
          setFilter={setComputercode}
          setApplyFilter={setApplyComputerCodeFilter}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="نوع مالکیت"
          inputKind="select"
          filter={OwnershipTypeRefId}
          setFilter={setOwnershipTypeRefId}
          dropdownDictionary={ownershipTypesDictionary}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="استان"
          inputKind="select"
          filter={ProvinceRefId}
          setFilter={setProvinceRefId}
          dropdownDictionary={provincesDictionary}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="شهرستان"
          inputKind="select"
          filter={CountyRefId}
          setFilter={setCountyRefId}
          dropdownDictionary={countiesDictionary}
          selectedProvinceId={ProvinceRefId}
          countyToProvinceIdDictionary={countyToProvinceId_Dictionary}
        />
      </th>
      <th scope="col" id="addressCol">
        <PopoverDialog
          id={popoverId++}
          label="آدرس"
          inputKind="input"
          filter={Address}
          setFilter={setAddress}
          setApplyFilter={setApplyAddressFilter}
        />
      </th>
      <th scope="col" id="addressCol">
        <PopoverDialog
          id={popoverId++}
          label="موقعیت جغرافیایی"
          inputKind="input"
          filter={GeographicPosition}
          setFilter={setGeographicPosition}
          setApplyFilter={setApplyGeographicPositionFilter}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="میزان سرمایه گذاری"
          inputKind="interval"
          filter={InvestmentAmountLowerBound}
          setFilter={setInvestmentAmountLowerBound}
          upperBoundFilter={InvestmentAmountUpperBound}
          setUpperBoundFilter={setInvestmentAmountUpperBound}
          setApplyFilter={setApplyInvestmentFilter}
        />
      </th>
      <th scope="col">
        {' '}
        <PopoverDialog
          id={popoverId++}
          label="درجه"
          inputKind="interval"
          filter={DegreeLowerBound}
          setFilter={setDegreeLowerBound}
          upperBoundFilter={DegreeUpperBound}
          setUpperBoundFilter={setDegreeUpperBound}
          setApplyFilter={setApplyDegreeFilter}
        />
      </th>
      <th scope="col">
        {' '}
        <PopoverDialog
          id={popoverId++}
          label="مساحت ناحیه"
          inputKind="interval"
          filter={AreaLowerBound}
          setFilter={setAreaLowerBound}
          upperBoundFilter={AreaUpperBound}
          setUpperBoundFilter={setAreaUpperBound}
          setApplyFilter={setApplyAreaFilter}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="نوع معدن"
          inputKind="select"
          filter={MineTypeRefId}
          setFilter={setMineTypeRefId}
          dropdownDictionary={mineTypesDictionary}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="وضعیت"
          inputKind="select"
          filter={StatusRefId}
          setFilter={setStatusRefId}
          dropdownDictionary={statusesDictionary}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="تعهد اشتغال"
          inputKind="radio"
          filter={EmploymentCommitment}
          setFilter={setEmploymentCommitment}
        />
      </th>
      <th scope="col">
        <PopoverDialog
          id={popoverId++}
          label="شماره معدن"
          inputKind="input"
          filter={PhoneNumber}
          setFilter={setPhoneNumber}
          setApplyFilter={setApplyPhoneNumberFilter}
        />
      </th>
    </tr>
  )
}

export default MinesHeaders
