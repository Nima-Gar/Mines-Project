import {Flip, ToastContainer} from 'react-toastify'
import {Route, Routes} from 'react-router-dom'

import MainLayout from './layouts/MainLayout';
import AddSection from './adding_components/AddSection';
import ListSection from './listing_components/ListSection';

function App() {
  return (
      <MainLayout>
        <Routes>
          <Route path='/add-mine' Component={AddSection} />
          <Route path='/' exact Component={ListSection} />
        </Routes>
        <ToastContainer rtl position='top-right' autoClose={2000} pauseOnHover={true} theme='light' transition={Flip} />
      </MainLayout>
  );
}

export default App;
