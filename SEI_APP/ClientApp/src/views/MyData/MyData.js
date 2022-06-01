import React, { useState, useEffect,images,FileUploader } from 'react';
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
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser, cilPhone, cilAddressBook, cilBirthdayCake } from '@coreui/icons'


function MyData(props) {
  const [tipoDocumento, setTipoDocumento] = useState([]);
    const [userInfo, setUser] = useState({
      firstname: '',
      lastname: '',
      username: '',
      typedocument: '',
      document: '',
      birthdate: '',
      phone: '',
      address: '',
      email: '',
      password: '',
      photo:''
    });

    const GetTipoDocumento = async () => {
      await axios.get("https://localhost:44342/users/getDocumentType")
        .then(response => {
          setTipoDocumento(response.data)
        })
        .catch((error) => {
          console.log(error);
        });
    }

  const GetInfoUsuario = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    await axios.get("https://localhost:44342/users/getInfoUser?idUser=" + infoUser.idUser)
      .then(response => {
        setUser(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }
  useEffect(() => {
    GetTipoDocumento();
    GetInfoUsuario();
  }, [])

  let tipoDocumentoList = tipoDocumento.length > 0
    && tipoDocumento.map((item, i) => {
      return (
        <option key={i} value={item.nombre}>{item.nombre}</option>
      )
    }, this);

    const apiUrl = "https://localhost:44342/users/updateInfo";
    const MyData = (e) => {
      e.preventDefault();
      const data = {
        firstname: userInfo.firstname,
        lastname: userInfo.lastname,
        username: userInfo.username,
        typedocument: userInfo.typedocument,
        document: userInfo.document,
        birthdate: userInfo.birthdate,
        phone: userInfo.phone,
        address: userInfo.address,
        email: userInfo.email,
        password: userInfo.password,
        photo:userInfo.photo
      };
      axios.post(apiUrl, data)
        .then((result) => {
          debugger;
          console.log(result.data);
          const serializedState = JSON.stringify(result.data.UserDetails);
          var a = localStorage.setItem('MyData', serializedState);
          console.log("A:", a)
          const user = result.data.token;
          console.log(user);
          if (result.status == 200)
            window.location.href = '/Dashboard';
          else
            alert('No registrado');
        })
    };
    const onChange = (e) => {
      e.persist();
      setUser({ ...userInfo, [e.target.name]: e.target.value });
    }
    return (
      <div className="bg-light min-vh-50 d-flex flex-row align-items-center">
        <CContainer>
          <CRow className="justify-content-center">
            <CCol md={9} lg={7} xl={6}>
              <CCard className="mx-4">
                <CCardBody className="p-4">
                  <CForm onSubmit={MyData} className="user">
                    <h2>Mis Datos</h2>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput placeholder="Nombres" value={userInfo.firstName} onChange={onChange} autoComplete="firstname" name="firstname" id="firstname" />
                    </CInputGroup>

                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput placeholder="Apellidos" value={userInfo.lastname} onChange={onChange} autoComplete="lastname" name="lastname" id="lastname" />
                    </CInputGroup>

                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormSelect aria-label="Default select example" name="typedocument" id="typedocument">
                        {tipoDocumentoList}
                      </CFormSelect>
                    </CInputGroup>

                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput placeholder="Número De Documento" value={userInfo.document} onChange={onChange} autoComplete="document" name="document" id="document" />
                    </CInputGroup>

                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilBirthdayCake} />
                      </CInputGroupText>
                      <CFormInput type="date" placeholder="Fecha de nacimiento" value={userInfo.birthdate} onChange={onChange} name="birthdate" id="birthdate" />
                    </CInputGroup>

                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilPhone} />
                      </CInputGroupText>
                      <CFormInput placeholder="Telefono" value={userInfo.phone} onChange={onChange} autoComplete="phone" name="phone" id="phone" />
                    </CInputGroup>

                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilAddressBook} />
                      </CInputGroupText>
                      <CFormInput placeholder="Dirección" value={userInfo.address} onChange={onChange} autoComplete="address" name="address" id="address" />
                    </CInputGroup>
                    <div className="d-grid">
                      <CButton color="success" type="submit" >Actualizar Datos</CButton>
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

  export default MyData

