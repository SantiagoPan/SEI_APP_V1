import React, { useState } from 'react';
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
import { cilUser, cilAddressBook, cilBank, cilCreditCard, cilDialpad } from '@coreui/icons'


function RegisterBank1(props) {
  const [bankInfo, setBankInfo] = useState({
    IdUser : '',
    Bank: '',
    TypeAccount: '',
    NumberAccount: '',
  });

    
    const SaveBank = (e) => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    bankInfo.IdUser = infoUser.idUser
      e.preventDefault();
      const apiUrl = "https://localhost:44342/users/saveBankInfo";
      const data = {
        IdUser: bankInfo.IdUser,
        Bank: bankInfo.Bank,
        TypeAccount: bankInfo.TypeAccount,
        NumberAccount: bankInfo.NumberAccount
      };
      axios.post(apiUrl, data)
        .then((result) => {
          console.log(result.data);
          if (result.status == 200)
             window.location.href = '#/Billing';
          else
            alert("Información bancaria no registrada");
        })
    };
    const onChange = (e) => {
      e.persist();
      setBankInfo({ ...bankInfo, [e.target.name]: e.target.value });
    }
    return (
      <div className="bg-light min-vh-50 d-flex flex-row align-items-center">
        <CContainer >
          <CRow className="justify-content-center">
            <CCol md={9} lg={7} xl={7}>
              <CCard className="mx-20">
                <CCardBody className="p-4">
                  <CForm onSubmit={SaveBank} className="user">

                    <h3>  <center> REGISTRO DE CUENTA BANCARIA </center> </h3>
                    <fieldset> <p className="text-medium-emphasis">DATOS DE LA CUENTA</p>
                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilBank} />
                        </CInputGroupText>
                        <CFormSelect aria-label="Default select example" required onChange={onChange} value={bankInfo.Bank} name="Bank" id="Bank">
                          <option>Entidad Bancaria</option>
                          <option value="Bancolombia">Bancolombia</option>
                          <option value="Nequi">Nequi</option>
                          <option value="BBVA">BBVA</option>
                          <option value="Davivienda">Davivienda</option>
                        </CFormSelect>
                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilCreditCard} />
                        </CInputGroupText>
                        <CFormSelect aria-label="Default select example" required onChange={onChange} value={bankInfo.TypeAccount} name="TypeAccount" id="TypeAccount">
                          <option>Tipo De Cuenta</option>
                          <option value="Ahorros">Ahorros</option>
                          <option value="Corriente">Corriente</option>
                        </CFormSelect>
                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilDialpad} />
                        </CInputGroupText>
                        <CFormInput placeholder="Numero De Cuenta" required value={bankInfo.NumberAccount} onChange={onChange} autoComplete="accountNumber" name="NumberAccount" id="NumberAccount" />
                      </CInputGroup>
                    </fieldset>

                    <div className="d-grid">
                      <CButton color="success" type="submit"> Guardar</CButton>
                      <CButton color="primary" className="mt-3"> Cancelar</CButton>
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
  
export default RegisterBank1
