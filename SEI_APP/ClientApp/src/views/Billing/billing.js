import React, { useState, useEffect, images, FileUploader } from 'react';
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
  CFormTextarea,
  cilMagnifyingGlass,


} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilSearch, cilLockLocked, cilUser, cilPhone, cilAddressBook, cilBirthdayCake } from '@coreui/icons'
/*import OfferService from '../services/offerservice/OfferService';*/


function Billing(props) {
  const [billinginfo, setUser] = useState({
    titulooferta: '',
    departamento: '',
    barrio: '',
    categoria: '',
    descripcionoferta: '',

  });
  const apiUrl = "https://localhost:44342/services/register";
  const Offerservi = (e) => {
    e.preventDefault();
    const data = {
      titulooferta: Offerservices.titulooferta,
      departamento: Offerservices.departamento,
      barrio: Offerservices.barrio,
      categoria: Offerservices.categoria,
      descripcionoferta: Offerservices.descripcionoferta

    };
    axios.post(apiUrl, data)
      .then((result) => {
        debugger;
        console.log(result.data);
        const serializedState = JSON.stringify(result.data.UserDetails);
        var a = localStorage.setItem('OfferService', serializedState);
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
    setUser({ ...Offerservices, [e.target.name]: e.target.value });
  }
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
                  <CButton color="success" type="submit" size="lg" >AGREGAR CUENTA</CButton>
                </a>
              </div>
            </CCardBody>
          </CCard>

        </CRow>
      </CContainer>
    </div>

  )

}

export default Billing
