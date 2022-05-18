import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CFormSelect,
  CInputGroup,
  CInputGroupText,
  CRow,
  CModal,
  CModalBody,
  CModalHeader,
  CModalFooter,
  CModalTitle,
} from '@coreui/react'

import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser, cilPhone, cilAddressBook, cilBirthdayCake } from '@coreui/icons'

function Register1(props) {
  const [tipoDocumento, setTipoDocumento] = useState([]);
  const [userInfo, setUser] = useState({
    firstname: '',
    lastname: '',
    username: '',
    documenttype: '',
    document: '',
    birthdate: '',
    phone: '',
    address: '',
    email: '',
    password: ''
  });
  const [visible, setVisible] = useState(false)
  const apiUrl = "https://localhost:44342/users/register";
  const Register = (e) => {
    e.preventDefault();
    const data = {
      firstname: userInfo.firstname,
      lastname: userInfo.lastname,
      username: userInfo.username,
      documenttype: userInfo.documenttype,
      document: userInfo.document,
      birthdate: userInfo.birthdate,
      phone: userInfo.phone,
      address: userInfo.address,
      email: userInfo.email,
      password: userInfo.password
    };
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        const serializedState = JSON.stringify(result.data.UserDetails);
        var a = localStorage.setItem('myData', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);
        if (result.status == 200) {
          setVisible(!visible)
        }
        else {
          alert('No registrado');
        }
          
      })
  };
  const GetTipoDocumento = async () => {
    await axios.get("https://localhost:44342/users/getDocumentType")
      .then(response => {
        setTipoDocumento(response.data)
        setTimeout(() => {
          console.log(tipoDocumento);
        }, 3000)
      })
      .catch((error) => {
        console.log(error);
      });
  }
  useEffect(() => {
    GetTipoDocumento();
  }, [])

  let tipoDocumentoList = tipoDocumento.length > 0
    && tipoDocumento.map((item, i) => {
      return (
        <option key={i} value={item.nombre}>{item.nombre}</option>
      )
    }, this);

  const onChange = (e) => {
    e.persist();
    setUser({ ...userInfo, [e.target.name]: e.target.value });
  }



  return (

    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>

        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={6}>
            <CModal visible={visible} onClose={() => setVisible(false)}>
              <CModalHeader onClose={() => setVisible(false)}>
                <CModalTitle>Bienvenido a SEI</CModalTitle>
              </CModalHeader>
              <CModalBody>Se ha registrado exitosamente en SEI</CModalBody>
              <CModalFooter>
                <CButton color="secondary" onClick={() => window.location.href = '#/Login'}>
                  Aceptar
                </CButton>
              </CModalFooter>
            </CModal>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <CForm onSubmit={Register} className="user">
                  <h1>Registro de Usuario</h1>
                  <p className="text-medium-emphasis">Crea tu cuenta</p>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput required placeholder="Nombres" value={userInfo.firstname} onChange={onChange} autoComplete="firstname" name="firstname" id="firstname" />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput required placeholder="Apellidos" value={userInfo.lastname} onChange={onChange} autoComplete="lastname" name="lastname" id="lastname" />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput required placeholder="Nombre De Usuario" value={userInfo.username} onChange={onChange} autoComplete="username" name="username" id="username" />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormSelect required aria-label="Default select example" onChange={onChange} value={userInfo.documenttype} name="documenttype" id="documenttype">
                      {tipoDocumentoList }
                    </CFormSelect>
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput required placeholder="Número De Documento" value={userInfo.document} onChange={onChange} autoComplete="document" name="document" id="document"/>
                  </CInputGroup>  

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilBirthdayCake} />
                    </CInputGroupText>
                    <CFormInput required type="date" placeholder="Fecha de nacimiento" value={userInfo.birthdate} onChange={onChange} name="birthdate" id="birthdate"  />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilPhone} />
                    </CInputGroupText>
                    <CFormInput required placeholder="Telefono" value={userInfo.phone} onChange={onChange} autoComplete="phone" name="phone" id="phone" />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilAddressBook} />
                    </CInputGroupText>
                    <CFormInput required placeholder="Dirección" value={userInfo.address} onChange={onChange} autoComplete="address" name="address" id="address" />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>@</CInputGroupText>
                    <CFormInput required placeholder="Correo" value={userInfo.email} onChange={onChange} autoComplete="email" name="email" id="email" />
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilLockLocked} />
                    </CInputGroupText>
                    <CFormInput required type="password" value={userInfo.password} onChange={onChange} placeholder="Contraseña" name="password" id="password"  autoComplete="new-password"/>
                  </CInputGroup>
                
                  <div className="d-grid">
                    <CButton color="success" type="submit" > Registrarse</CButton>
                  </div>
                </CForm>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Register1
