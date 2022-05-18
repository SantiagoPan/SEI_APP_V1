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
  CCardImage,
  CCardTitle,
  CCardText,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilSearch, cilLockLocked, cilUser, cilPhone, cilAddressBook, cilBirthdayCake } from '@coreui/icons'
/*import OfferService from '../services/offerservice/OfferService';*/


function post(props) {
  const [Offerservices, setUser] = useState({
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
      <CContainer className="px-12">
        <CRow xs={{ gutterX: 3}}>
          <CCol>
     
                       
            <CCard style={{ width: '30rem' }}>
              <CCardImage orientation="top" src="https://www.lupamarketing.com.ar/wp-content/uploads/2020/07/Digital-Marketing-01.png" />
            <CCardBody>
              <CCardTitle>¡Haz publico tu Servicio!</CCardTitle>
              <CCardText>
                Aqui podras dar a conocer el servicio que quieres ofrecer!
              </CCardText>
                <CButton color="dark" href="#/OfferService">Publicar Servico</CButton>
              </CCardBody>
          </CCard>
          </CCol>

          <CCard style={{ width: '30rem' }}>
            <CCardImage orientation="top" src="https://blog.ida.cl/wp-content/uploads/sites/5/2020/11/ida-herramientasContentDigital-blog-1-655x470.png" />
            <CCardBody>
              <CCardTitle>¡Haz publico tu Producto!</CCardTitle>
              <CCardText>
                Aqui podras dar a conocer el producto que quieres ofrecer!
              </CCardText>
              <CButton color="dark" href="#/OfferProduct">Publicar Producto</CButton>
            </CCardBody>

          </CCard>
        </CRow>
      </CContainer>
    </div>


)
}

export default post
