import React from 'react'
import { CFooter } from '@coreui/react'

const AppFooter = () => {
  return (
    <CFooter>
      <div>
        <a href="https://www.sei.com.co" target="_blank" rel="noopener noreferrer">
          SEI (Sistema de empleo informal)
        </a>
        <span className="ms-1">&copy; 2022</span>
      </div>
      <div className="ms-auto">
        <a href="https://www.sei.com.co" target="_blank" rel="noopener noreferrer">
        </a>
      </div>
    </CFooter>
  )
}

export default React.memo(AppFooter)
