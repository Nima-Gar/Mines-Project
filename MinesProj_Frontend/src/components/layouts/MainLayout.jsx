import React from 'react'
import Sidebar from './../common/Sidebar'

const MainLayout = ({ children }) => {
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-md-10 col-12 p-0">{children}</div>
        <div className="d-none d-md-block col-md-2 p-0" id='sidebarSection'>
          <Sidebar />
        </div>
      </div>
    </div>
  )
}

export default MainLayout
