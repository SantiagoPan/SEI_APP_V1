import React, { useState } from 'react'
import axios from 'axios';
import { Link } from 'react-router-dom'
import {
  CButton,
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CAlert,
  CInputGroupText,
  CRow,
  CModal,CModalTitle,CModalBody,CModalFooter,CModalHeader
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser } from '@coreui/icons'

function Login1(props) {

  const [visible, setVisible] = useState(false)
  const [userInfo, setUser] = useState({ email: '', password: '' });

  const Login = (e) => {
    e.preventDefault();
    const data = { email: userInfo.email, password: userInfo.password };
    const apiUrl = "https://localhost:44342/users/login";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log("result.data ", result.data);
        const serializedState = JSON.stringify(result.data);
        var a = localStorage.setItem('myData', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);

        if (result.status == 200 && result.data != 'LoginError') {
           window.location.href = '#/Dashboard';
        }
        else {
          setVisible(true)
        }
      })
    
  };
  const onChange = (e) => {
    e.persist();
    setUser({ ...userInfo, [e.target.name]: e.target.value });
  }
  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={8}>
            <CCardGroup>
              <CCard className="p-4">
                <CModal visible={visible} onClose={() => setVisible(false)}>
                  <CModalHeader onClose={() => setVisible(false)}>
                    <CModalTitle>SEI</CModalTitle>
                  </CModalHeader>
                  <CModalBody>El usuario y/o contraseña son incorrectos, por favor intente nuevamente</CModalBody>
                  <CModalFooter>
                    <CButton color="danger" onClick={() => setVisible(false)}>
                      Aceptar
                    </CButton>
                  </CModalFooter>
                </CModal>
                <CCardBody>
                  <CForm onSubmit={Login} className="user">
                    <h1>SEI</h1>
                    <p className="text-medium-emphasis">Sistema De Empleo Informal</p>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>@</CInputGroupText>
                      <div className="form-group">
                      <CFormInput type="email" className="form-control" value={userInfo.email} onChange={onChange} name="email" id="email" aria-describedby="emailHelp" placeholder="Enter Email"  required/>
                        </div>
                        </CInputGroup>
                    <CInputGroup className="mb-4">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <div className="form-group">
                      <CFormInput type="password" className="form-control" value={userInfo.password} onChange={onChange} name="password" id="password" placeholder="Password" required/>
                      </div>
                        </CInputGroup>
                    <CRow>
                      <CCol xs={6}>
                        <CButton color="primary" type="submit" className="px-4">
                          Entrar
                        </CButton>
                      </CCol>
                      <CCol xs={6} className="text-right">
                        <CButton color="link" className="px-0">
                          Restablecer Contraseña
                        </CButton>
                      </CCol>
                    </CRow>
                  </CForm>
                </CCardBody>
              </CCard>
              <CCard className="text-white bg-primary py-5" style={{ width: '44%' }}>
                <CCardBody className="text-center">
                  <div>
                    <h2>Inscribete!</h2>
                    <p>
                      Te ayudamos a encontrar el empleo informal  ideal para ti de forma rápida.
                    </p>
                    <Link to="/register">
                      <CButton color="primary" className="mt-3" active tabIndex={-1}>
                        Registrarse
                      </CButton>
                    </Link>
                  </div>
                </CCardBody>
              </CCard>
            </CCardGroup>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Login1
