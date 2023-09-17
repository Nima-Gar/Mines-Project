import http from "./httpService";
import config from "./config.json"

export const getNumTypes = () => http.get(`${config.localapi}/api/PhoneNumTypes/GetPhoneNumTypes`).then(resp => resp.data)
export const getOwnershipTypes = () => http.get(`${config.localapi}/api/OwnershipTypes/GetOwnershipTypes`).then(resp => resp.data)
export const getProvinces = () => http.get(`${config.localapi}/api/Provinces/GetProvinces`).then(resp => resp.data)
export const getCounties = () => http.get(`${config.localapi}/api/Counties/GetCounties`).then(resp => resp.data)
export const getMineTypes = () => http.get(`${config.localapi}/api/MineTypes/GetMineTypes`).then(resp => resp.data)
export const getStatuses = () => http.get(`${config.localapi}/api/Statuses/GetStatuses`).then(resp => resp.data)