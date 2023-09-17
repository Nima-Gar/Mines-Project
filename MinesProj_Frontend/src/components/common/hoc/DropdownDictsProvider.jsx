import { useEffect } from 'react'

import dropdownDictsContext from '../../contexts/dropdownDictsContext'
import {
  getNumTypes,
  getOwnershipTypes,
  getProvinces,
  getCounties,
  getMineTypes,
  getStatuses,
} from '../../../services/dropdownsService'
import { useState } from 'react'

const DropdownDictsProvider = ({ children }) => {
  const [numTypesDictionary, setNumTypesDictionary] = useState({})
  const [ownershipTypesDictionary, setOwnershipTypesDictionary] = useState({})
  const [provincesDictionary, setProvincesDictionary] = useState({})
  const [countiesDictionary, setCountiesDictionary] = useState({})
  const [mineTypesDictionary, setMineTypesDictionary] = useState({})
  const [statusesDictionary, setStatusesDictionary] = useState({})
  const [countyToProvinceId_Dictionary, setCountyToProvinceId_Dictionary] =
    useState({})

  useEffect(() => {
    fillDictionaries()
  }, [])

  const fillDictionaries = () => {
    fillDictionary('numTypes', setNumTypesDictionary, false)
    fillDictionary('ownershipTypes', setOwnershipTypesDictionary, false)
    fillDictionary('provinces', setProvincesDictionary, false)
    fillDictionary(
      'counties',
      setCountiesDictionary,
      true,
      setCountyToProvinceId_Dictionary
    )
    fillDictionary('mineTypes', setMineTypesDictionary, false)
    fillDictionary('statuses', setStatusesDictionary, false)
  }

  const fillDictionary = async (
    getRequestTitle,
    setDictionary,
    fillCountyToProvinceId_Dict,
    setCountyToProvinceId_Dictionary = null
  ) => {
    let requestToGet = () => {}
    switch (getRequestTitle) {
      case 'numTypes':
        requestToGet = getNumTypes
        break
      case 'ownershipTypes':
        requestToGet = getOwnershipTypes
        break
      case 'provinces':
        requestToGet = getProvinces
        break
      case 'counties':
        requestToGet = getCounties
        break
      case 'mineTypes':
        requestToGet = getMineTypes
        break
      case 'statuses':
        requestToGet = getStatuses
        break
    }

    const itemsList = await requestToGet()
    let dictionary = {}
    itemsList.map((item) => (dictionary[item.id] = item.title))
    setDictionary(dictionary)

    if (fillCountyToProvinceId_Dict && setCountyToProvinceId_Dictionary) {
      dictionary = {}
      itemsList.map((item) => (dictionary[item.id] = item.provinceRefId))
      setCountyToProvinceId_Dictionary(dictionary)
    }
  }

  return (
    <dropdownDictsContext.Provider
      value={{
        numTypesDictionary,
        ownershipTypesDictionary,
        provincesDictionary,
        countiesDictionary,
        mineTypesDictionary,
        statusesDictionary,
        countyToProvinceId_Dictionary,
      }}
    >
      {children}
    </dropdownDictsContext.Provider>
  )
}

export default DropdownDictsProvider
