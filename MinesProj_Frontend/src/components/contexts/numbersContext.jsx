import { createContext } from 'react';

const numbersContext = createContext({
    numTypesDictionary: {},
    idToDelete: 0,
    setIdToDelete: () => {},
    setCurrentMinePhonenums: () => {},
    handleNewNumber: () => {},
    deleteNumber: () => {},
})

export default numbersContext