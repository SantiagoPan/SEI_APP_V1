import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  cilCart,
  CCol,
  CContainer,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableDataCell,
  CTableHeaderCell,
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

function MySales(props) {
  const [InfoSales, setInfoSales] = useState([]);

  const GetSalesPS = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/services/getServicesAndProductsById?idUser=" + infoUser.idUser)
      .then(response => {
        setInfoSales(response.data);
        console.log(InfoSales);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetSalesPS();
  }, [])

  return (
    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={12}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <h2>Mis Ventas</h2>
              </CCardBody>
              <div className="justify-content-center">
              <CTable bordered>
                <CTableHead>
                  <CTableRow>
                    <CTableHeaderCell scope="col">#</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Producto/Servicio</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Valor</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Fecha Venta</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Comprador</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Tipo</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Estado</CTableHeaderCell>
                  </CTableRow>
                </CTableHead>
                <CTableBody>
                  {InfoSales &&
                    InfoSales.map((venta) => (
                      <CTableRow>
                        <CTableHeaderCell scope="row">{1+1}</CTableHeaderCell>
                        <CTableDataCell>{venta.nombre}</CTableDataCell>
                        <CTableDataCell>{venta.precio}</CTableDataCell>
                        <CTableDataCell>{venta.fechaVenta}</CTableDataCell>
                        <CTableDataCell>{venta.nombreComprador}</CTableDataCell>
                        <CTableDataCell>{venta.tipo}</CTableDataCell>
                        <CTableDataCell>{venta.estadoVenta}</CTableDataCell>
                      </CTableRow>
                    ))}
                </CTableBody>
              </CTable>
                </div>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
    )
}

export default MySales
