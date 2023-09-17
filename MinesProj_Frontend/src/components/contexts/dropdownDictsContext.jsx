import { createContext } from "react";

const dropdownDictsContext = createContext({
    numTypesDictionary : {},
    ownershipTypesDictionary : {},
    provincesDictionary : {},
    countiesDictionary : {},
    mineTypesDictionary : {},
    statusesDictionary : {},
    countyToProvinceId_Dictionary : {},
})

export default dropdownDictsContext;