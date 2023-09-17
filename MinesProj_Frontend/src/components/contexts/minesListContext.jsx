import { createContext } from "react";

const minesListContext = createContext({
    mines: [],
    currentPageMines: [],
    setMines: () => {},
})

export default minesListContext;