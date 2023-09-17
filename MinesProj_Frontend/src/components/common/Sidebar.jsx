import React, { useState } from 'react'

const Sidebar = () => {
  const [firstChervonClass, setFirstChervonClass] = useState('fa-chevron-left')
  const [secondChervonClass, setSecondChervonClass] = useState('fa-chevron-left')

  const changeChevronDirection = (nth) => {
    if (nth === 1)
      setFirstChervonClass(
        firstChervonClass === 'fa-chevron-left' ? 'fa-chevron-down' : 'fa-chevron-left'
      )
    else
      setSecondChervonClass(
        secondChervonClass === 'fa-chevron-left' ? 'fa-chevron-down' : 'fa-chevron-left'
    )     
    
  }

  return (
    <>
      <div className="main w-100 h-100 bg-defoult rounded">
        <aside>
          <div className="sidebar w-100">
            <ul className="list-sidebar">
              <li className="text-center offset-md-2 col-md-9 pt-3 px-1">
                <p className="py-1">
                  سامانه <strong>معادن</strong>
                </p>
                <hr />
              </li>

              <li>
                {' '}
                <a
                  onClick={() => changeChevronDirection(1)}
                  href="#"
                  data-toggle="collapse"
                  data-target="#dashboard"
                  className="collapsed active d-flex justify-content-between"
                >
                  {' '}
                  <i className={`fa  ${firstChervonClass}`}/>{' '}
                  <div>
                    <span className="nav-label"> دسترسی سریع </span>{' '}
                    <i className="fa fa-home" />{' '}
                  </div>
                </a>
                <ul className="sub-menu collapse" id="dashboard">
                  <li className="active">
                    <a href="#">CSS3 Animation</a>
                  </li>
                  <li>
                    <a href="#">General</a>
                  </li>
                  <li>
                    <a href="#">Tabs & Accordions</a>
                  </li>
                  <li>
                    <a href="#">Typography</a>
                  </li>
                  <li>
                    <a href="#">Slider</a>
                  </li>
                  <li>
                    <a href="#">Widgets</a>
                  </li>
                </ul>
              </li>

              <li>
                {' '}
                <a
                  onClick={() => changeChevronDirection(2)}
                  href="#"
                  data-toggle="collapse"
                  data-target="#products"
                  className="collapsed active d-flex justify-content-between"
                >
                  {' '}
                  <i className={`fa  ${secondChervonClass}`} />
                  <div>
                    <span className="nav-label">ابزار</span>
                    <i className="fa fa-wrench"></i>{' '}
                  </div>
                </a>
                <ul className="sub-menu collapse" id="products">
                  <li className="active">
                    <a href="#">CSS3 Animation</a>
                  </li>
                  <li>
                    <a href="#">General</a>
                  </li>
                  <li>
                    <a href="#">Tabs & Accordions</a>
                  </li>
                  <li>
                    <a href="#">Typography</a>
                  </li>
                  <li>
                    <a href="#">Slider</a>
                  </li>
                  <li>
                    <a href="#">Widgets</a>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
        </aside>
      </div>
    </>
  )
}

export default Sidebar
