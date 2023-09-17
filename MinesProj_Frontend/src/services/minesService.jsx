import http from './httpService'
import config from './config.json'

export const getMines = () =>
  http.get(`${config.localapi}/api/mines/GetDetailedMines`).then((resp) => resp.data)

export const getFilteredtMines = (filters) =>
  http.post(`${config.localapi}/api/Mines/GetFilteredDetailedMines`, JSON.stringify(filters)).then((resp) => resp.data)

export const addMine = (mine) =>
  http.post(`${config.localapi}/api/mines/PostMine`, JSON.stringify(mine))
    .then((resp) => resp)

export const deleteMine = (mineId) =>
  http.delete(`${config.localapi}/api/mines/DeleteMine?id=${mineId}`)