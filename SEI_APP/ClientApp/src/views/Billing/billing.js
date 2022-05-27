import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  cilCart,
  CCol,
  CContainer,
  CCardTitle,
  CForm,
  CFormInput,
  CFormSelect,
  CInputGroup,
  CInputGroupText,
  CRow,
  CCardText,
  CFormTextarea,
  cilMagnifyingGlass,


} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilSearch, cilLockLocked, cilUser, cilPhone, cilAddressBook, cilBirthdayCake } from '@coreui/icons'

function Billing(props) {
  const [infoBank, setDataBank] = useState([]);

  const GetInfoBank = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/users/getInfoBank?idUser=" + infoUser.idUser)
      .then(response => {
        setDataBank(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetInfoBank();
  }, [])

  return (

    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer className="px-50">
        <CRow xs={{ gutterX: 3 }}>
          <CCol>
          </CCol>
          <CCard style={{ width: '18rem' }}>
            <CCardBody>
              <div className="d-grid gap-2">
                <a href="#/registerBank">
                  <CButton color="success" type="submit" size="lg" >Agregar Cuenta Bancaria</CButton>
                </a>
              </div>
            </CCardBody>
          </CCard>

          <CCard style={{ width: '18rem' }}>
            <div className="row">
              {infoBank &&
                infoBank.map((account) => (
                  <CCard style={{ width: '18rem' }} key={account.id}>
                    <CCardBody>
                      <CCardTitle> Cuenta Bancaria</CCardTitle>
                      <br></br>
                      <CCardText><strong>Banco: </strong> {account.bank}</CCardText>
                      <CCardText><strong>Tipo de cuenta: </strong> {account.typeAccount}</CCardText>
                      <CCardText><strong>Numero de cuenta: </strong> {account.numberAccount}</CCardText>
                    </CCardBody>
                  </CCard>
                ))}
            </div>
          </CCard>



        </CRow>
      </CContainer>
    </div>

  )

}

export default Billing
