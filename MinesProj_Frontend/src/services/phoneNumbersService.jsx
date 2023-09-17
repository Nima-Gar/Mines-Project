import http from './httpService'
import config from './config.json'

export const getNumbers = () =>
  http
    .get(`${config.localapi}/api/phonenumbers/GetPhonenumbers`)
    .then((resp) => resp)

export const addNumber = (number) =>
  http.post(
    `${config.localapi}/api/phonenumbers/PostPhoneNumber`,
    JSON.stringify(number)
  )

export const deleteNumber = (numberId) =>
  http.delete(
    `${config.localapi}/api/phonenumbers/DeletePhoneNumber?id=${numberId}`
  )
